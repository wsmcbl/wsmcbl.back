namespace wsmcbl.src.model.config;

public class RolePermissionEntity
{
    public int roleId { get; set; }
    public int permissionId { get; set; }

    public PermissionEntity? permission { get; set; }
}