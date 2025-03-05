namespace wsmcbl.src.model.config;

public class UserPermissionEntity
{
    public Guid userId { get; set; }
    public int permissionId { get; set; }

    public PermissionEntity? permission { get; set; }

    public UserPermissionEntity()
    {
    }

    public UserPermissionEntity(Guid userId, int permissionId)
    {
        this.userId = userId;
        this.permissionId = permissionId;
    }
    
    public bool equals(UserPermissionEntity value)
    {
        return userId == value.userId && permissionId == value.permissionId;
    }
}