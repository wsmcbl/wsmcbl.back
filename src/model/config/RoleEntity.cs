namespace wsmcbl.src.model.config;

public class RoleEntity
{
    public int roleId { get; set; }
    public string name { get; set; } = null!;
    public string description { get; set; } = null!;
    
    public List<PermissionEntity> permissionList { get; set; } = [];

    public string getSpanishName()
    {
        return name switch
        {
            "admin" => "Administrador",
            "secretary" => "Secretario",
            "cashier" => "Cajero",
            "teacher" => "Docente",
            _ => "Sin roles"
        };
    }
}