namespace wsmcbl.src.model;

public class StudentView
{
    public string studentId { get; set; } = null!;
    public string fullName { get; set; } = null!;
    public bool isActive { get; set; }
    public string tutor { get; set; } = null!;
    public string? schoolyear { get; set; }
    public string? enrollment { get; set; }

    public void initLabels()
    {
        schoolyear ??= string.Empty;
        enrollment ??= "Sin matrícula";
    }
}