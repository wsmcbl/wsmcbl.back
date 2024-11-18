using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using wsmcbl.src.model.config;

namespace wsmcbl.src.dto.config;

public class UserDto
{
    [JsonRequired] public int roleId { get; set; }
    [Required] public string name { get; set; }
    public string? secondName { get; set; }
    [Required] public string surname { get; set; }
    public string? secondSurname { get; set; }
    [Required] public string email { get; set; }
    [Required] public bool isActive { get; set; }
    public string role { get; set; }

    public UserDto()
    {
    }
    
    public UserDto(UserEntity user)
    {
        roleId = user.roleId;
        name = user.name;
        secondName = user.secondName;
        surname = user.surname;
        secondSurname = user.secondSurname;
        email = user.email;
        isActive = user.isActive;
        role = user.role!.getSpanishName();
    }

    public UserEntity toEntity()
    {
        return new UserEntity
        {
            roleId = roleId,
            name = name,
            secondName = secondName,
            surname = surname,
            secondSurname = secondSurname,
            email = email,
            isActive = isActive
        };
    }
}