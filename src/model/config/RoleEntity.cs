namespace wsmcbl.src.model.config;

public class RoleEntity
{
    public int roleId { get; set; }
    public string name { get; set; } = null!;
    public string description { get; set; } = null!;
    
    public List<PermissionEntity> permissionList { get; set; } = [];

    public List<string> getPermissionList()
    {
        return permissionList.Select(e => e.name).ToList();
    }
}