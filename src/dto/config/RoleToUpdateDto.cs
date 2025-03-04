using wsmcbl.src.model.config;

namespace wsmcbl.src.dto.config;

public class RoleToUpdateDto
{
    public string description { get; set; } = null!;
    public List<BasicPermissionDto> permissionList { get; set; } = null!;
    
    public RoleEntity toEntity(int roleId)
    {
        var result = new RoleEntity
        {
            roleId = roleId,
            description = description,
            permissionList = getPermissionList(roleId)
        };

        return result;
    }

    private List<PermissionEntity> getPermissionList(int roleId)
    {
        throw new NotImplementedException();
    }
}