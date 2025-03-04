namespace wsmcbl.src.model.config;

public class PermissionEntity
{
    public int permissionId { get; set; }
    public string area { get; set; }
    public string name { get; set; }
    public string spanishName { get; set; }
    public string description { get; set; }

    public PermissionEntity(int id)
    {
        permissionId = id;
        area = string.Empty;
        name = string.Empty;
        spanishName = string.Empty;
        description = string.Empty;
    }
}