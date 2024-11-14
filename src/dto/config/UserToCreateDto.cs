using System.ComponentModel.DataAnnotations;
using wsmcbl.src.model.config;

namespace wsmcbl.src.dto.config;

public class UserToCreateDto
{
    [Required] public string name { get; set; } = null!;
    public string? secondName { get; set; }
    [Required] public string surname { get; set; } = null!;
    public string? secondSurname { get; set; }
    [Required] public string email { get; set; } = null!;
    [Required] public string password { get; set; } = null!;
    
    
    public UserToCreateDto()
    {
    }

    public UserToCreateDto(UserEntity user)
    {
        name = user.name;
        secondName = user.secondName;
        surname = user.surname;
        secondSurname = user.secondsurName;
        email = user.email;
        password = "";
    }
    
    public UserEntity toEntity()
    {
        return new UserEntity
        {
            userId = $"{name}000",
            name = name,
            secondName = secondName,
            surname = surname,
            secondsurName = secondSurname,
            password = password,
            username = email,
            email = email
        };
    }
}