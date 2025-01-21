namespace wsmcbl.src.model.config;

public class PermissionEntity
{
    public int permissionId { get; set; }
    public string group { get; set; } = null!;
    public string name { get; set; } = null!;
    public string spanishName { get; set; } = null!;
    public string description { get; set; } = null!;
}