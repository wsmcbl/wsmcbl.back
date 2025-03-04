using wsmcbl.src.model.config;

namespace wsmcbl.src.dto.config;

public class RoleToUpdateDto
{
    public string description { get; set; } = null!;
    public List<int> permissionList { get; set; } = null!;
    
    public RoleEntity toEntity(int roleId)
    {
        var result = new RoleEntity
        {
            roleId = roleId,
            description = description,
            rolePermissionList = getPermissionList(roleId)
        };

        return result;
    }

    private List<RolePermissionEntity> getPermissionList(int roleId)
    {
        return permissionList
            .Select(id => new RolePermissionEntity { roleId = roleId, permissionId = id }).ToList();
    }
}