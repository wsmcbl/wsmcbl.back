namespace wsmcbl.back.dto.input;

public class StudentDto
{
    public string name { get; set; } = null!;
    public string? secondName { get; set; }
    public string surname { get; set; } = null!;
    public string? secondSurname { get; set; }
    public required bool sex { get; set; }
    public DateDto birthday { get; set; } = null!;
    public string? tutor { get; set; }
}