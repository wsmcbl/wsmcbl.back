namespace wsmcbl.src.model.config;

public class AdminEntity
{
    public Guid userId { get; set; }
    public string teacherId { get; set; } = null!;
    public string securityToken { get; set; } = null!;
}