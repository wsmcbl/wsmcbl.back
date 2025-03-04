using wsmcbl.src.model.config;

namespace wsmcbl.src.dto.config;

public class BasicPermissionDto
{
    public int permissionId { get; set; }
    public string? name { get; set; }
    public string? area { get; set; }

    public BasicPermissionDto()
    {
    }
    
    public BasicPermissionDto(PermissionEntity value)
    {
        permissionId = value.permissionId;
        name = value.spanishName;
        area = value.area;
    }

    public PermissionEntity toEntity()
    {
        return new PermissionEntity(permissionId);
    }
}