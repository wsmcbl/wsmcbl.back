namespace wsmcbl.src.model.config;

public class RoleEntity
{
    public int roleId { get; set; }
    public string name { get; set; } = null!;
    public string description { get; set; } = null!;

    public List<RolePermissionEntity> rolePermissionList { get; set; } = null!;

    public List<PermissionEntity> getPermissionList()
    {
        return rolePermissionList.Select(e => e.permission).ToList();
    }

    public void updateRolePermissionList(List<RolePermissionEntity> list, IRolePermissionDao rolePermissionDao)
    {
        throw new NotImplementedException();
    }
}