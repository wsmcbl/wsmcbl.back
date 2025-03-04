namespace wsmcbl.src.model.config;

public class RoleEntity
{
    public int roleId { get; set; }
    public string name { get; set; } = null!;
    public string description { get; set; } = null!;

    public List<RolePermissionEntity> rolePermissionList { get; set; } = null!;

    public List<PermissionEntity> getPermissionList()
    {
        return rolePermissionList.Select(e => e.permission!).ToList();
    }

    public void updateRolePermissionList(List<RolePermissionEntity> list, IRolePermissionDao rolePermissionDao)
    {
        foreach (var item in rolePermissionList)
        {
            if (!list.Any(e => e.roleId == item.roleId && e.permissionId == item.permissionId))
            {
                rolePermissionDao.delete(item);
            }
        }

        foreach (var item in list)
        {
            if (!rolePermissionList.Any(e => e.roleId == item.roleId && e.permissionId == item.permissionId))
            {
                rolePermissionDao.create(item);
            }
        }
    }

    public void setDescription(string value)
    {
        if (description != value)
        {
            description = value;
        }
    }
}