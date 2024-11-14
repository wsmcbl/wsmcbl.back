using System.ComponentModel.DataAnnotations;
using wsmcbl.src.model.config;

namespace wsmcbl.src.dto.config;

public class LoginDto
{
    public string? token { get; set; }
    [Required] public string email { get; set; } = null!;
    [Required] public string password { get; set; } = null!;
    
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