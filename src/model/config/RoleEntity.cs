namespace wsmcbl.src.model.config;

public class RoleEntity
{
    public int roleId { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public List<PermissionEntity> permissionList { get; set; } = [];
}