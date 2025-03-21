namespace wsmcbl.src.model.config;

public class PermissionEntity
{
    public int permissionId { get; set; }
    public string area { get; set; } = null!;
    public string name { get; set; } = null!;
    public string spanishName { get; set; } = null!;
    public string description { get; set; } = null!;

    public PermissionEntity()
    {
    }

    public PermissionEntity(int id)
    {
        permissionId = id;
        area = string.Empty;
        name = string.Empty;
        spanishName = string.Empty;
        description = string.Empty;
    }
}