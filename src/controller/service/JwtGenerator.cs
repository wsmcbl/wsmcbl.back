using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using wsmcbl.src.model.config;

namespace wsmcbl.src.controller.service;

public class JwtGenerator
{
    private readonly IConfiguration _configuration;

    public JwtGenerator(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string generateToken(UserEntity user)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY")!);

        var securityKey = new SymmetricSecurityKey(key);
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = getSubject(user),
            Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpiryMinutes"])),
            Issuer = jwtSettings["Issuer"],
            Audience = jwtSettings["Audience"],
            SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private static ClaimsIdentity getSubject(UserEntity user)
    {
        var claimList = new List<Claim>
        {
            new("userid", user.userId.ToString()!),
            new(ClaimTypes.Email, user.email),
            new(ClaimTypes.Role, user.getRole()),
            new("roleid", user.userRoleId!)
        };
        
        claimList.AddRange(user.getPermissionList().Select(permission
            => new Claim("Permission", permission)));

        return new ClaimsIdentity(claimList);
    }
}