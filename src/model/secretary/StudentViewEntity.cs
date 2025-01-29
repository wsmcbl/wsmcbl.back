namespace wsmcbl.src.model.secretary;

public class StudentViewEntity
{
    public string studentId { get; set; } = null!;
    public string fullName { get; set; } = null!;
    public bool isActive { get; set; }
    public string schoolyear { get; set; } = null!;
    public string enrollment { get; set; } = null!;
}