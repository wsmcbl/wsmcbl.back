using System.ComponentModel.DataAnnotations;
using wsmcbl.src.model.config;

namespace wsmcbl.src.dto.config;

public class UserDto
{
    public int roleId { get; set; }
    [Required] public string name { get; set; }
    public string? secondName { get; set; }
    [Required] public string surname { get; set; }
    public string? secondSurname { get; set; }
    
    public bool isActive { get; set; }
    public string? nextCloudGroup { get; set; }
    
    public List<int> permissionList { get; set; }

    
    public UserDto(UserEntity user, string nextcloudGroup)
    {
        name = user.name;
        secondName = user.secondName;
        surname = user.surname;
        secondSurname = user.secondSurname;
        isActive = user.isActive;
        roleId = user.roleId;
        permissionList = user.getPermissionIdList();
        nextCloudGroup = nextcloudGroup;
    }
}