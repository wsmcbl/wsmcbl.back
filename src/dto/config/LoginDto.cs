using System.ComponentModel.DataAnnotations;
using wsmcbl.src.model.config;

namespace wsmcbl.src.dto.config;

public class LoginDto
{
    [Required] public string email { get; set; } = null!;
    [Required] public string password { get; set; } = null!;
    public string? token { get; set; }
    
    public UserEntity toEntity()
    {
        return new UserEntity
        {
            email = email,
            password = password
        };
    }

    public LoginDto()
    {
    }

    public LoginDto(string token)
    {
        email = string.Empty;
        password = string.Empty;
        this.token = token;
    }
}