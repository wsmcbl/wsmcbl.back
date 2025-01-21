using wsmcbl.src.model.config;

namespace wsmcbl.src.dto.config;

public class BasicPermissionDto
{
    public int permissionId { get; set; }
    public string name { get; set; }
    public string group { get; set; }
    
    public BasicPermissionDto(PermissionEntity value)
    {
        permissionId = value.permissionId;
        name = value.spanishName;
        group = value.area;
    }
}