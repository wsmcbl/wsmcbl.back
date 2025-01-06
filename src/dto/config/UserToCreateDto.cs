using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using wsmcbl.src.model.config;

namespace wsmcbl.src.dto.config;

public class UserToCreateDto
{
    [JsonRequired] public int roleId { get; set; }
    [Required] public string name { get; set; } = null!;
    public string? secondName { get; set; }
    [Required] public string surname { get; set; } = null!;
    public string? secondSurname { get; set; }
    public string? email { get; set; }
    public string? password { get; set; }

    public List<int> permissionList { get; set; } = null!;
    
    public UserToCreateDto()
    {
    }

    public UserToCreateDto(UserEntity user)
    {
        roleId = user.roleId;
        name = user.name;
        secondName = user.secondName;
        surname = user.surname;
        secondSurname = user.secondSurname;
    }
    
    public UserEntity toEntity()
    {
        return new UserEntity.Builder()
            .setName(name)
            .setSecondName(secondName)
            .setSurname(surname)
            .setSecondSurname(secondSurname)
            .setRole(roleId)
            .build();
    }
}