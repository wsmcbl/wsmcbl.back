using wsmcbl.src.model.config;

namespace wsmcbl.src.dto.config;

public class RoleDto
{
    public int roleId { get; set; }
    public string name { get; set; } 
    public string description { get; set; }   
    public List<int> permissionList { get; set; }

    public RoleDto(RoleEntity role)
    {
        roleId = role.roleId;
        name = role.name;
        description = role.description;
        permissionList = role.getPermissionList().Select(e => e.permissionId).ToList();
    }
}