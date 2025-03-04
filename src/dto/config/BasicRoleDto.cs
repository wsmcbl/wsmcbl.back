using wsmcbl.src.model.config;

namespace wsmcbl.src.dto.config;

public class BasicRoleDto
{
    public int roleId { get; set; }
    public string name { get; set; }
    public string description { get; set; }

    public BasicRoleDto(RoleEntity role)
    {
        roleId = role.roleId;
        name = role.name;
        description = role.description;
    }
}