using System.ComponentModel.DataAnnotations;
using wsmcbl.src.model.config;

namespace wsmcbl.src.dto.config;

public class UserDto
{
    public int roleId { get; set; }
    [Required] public string name { get; set; } = null!;
    public string? secondName { get; set; }
    [Required] public string surname { get; set; } = null!;
    public string? secondSurname { get; set; }
    [Required] public string email { get; set; } = null!;
    public string? password { get; set; }
    [Required] public bool isActive { get; set; }

    public UserDto()
    {
    }
    
    public UserDto(UserEntity user)
    {
        name = user.name;
        secondName = user.secondName;
        surname = user.surname;
        secondSurname = user.secondSurname;
        email = user.email;
        password = user.password;
        isActive = user.isActive;
        roleId = user.roleId;
    }

    public UserEntity toEntity()
    {
        return new UserEntity
        {
            name = name.Trim(),
            secondName = secondName?.Trim(),
            surname = surname.Trim(),
            secondSurname = secondSurname?.Trim(),
            isActive = isActive
        };
    }
}