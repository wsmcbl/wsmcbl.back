namespace wsmcbl.back.dto.input;

public class StudentDto
{
    public string name { get; set; } = null!;
    public string? secondName { get; set; }
    public string surname { get; set; } = null!;
    public string? secondSurname { get; set; }
    public bool sex { get; set; }
    public DateOnlyDto birthday { get; set; } = null!;
    public string? tutor { get; set; }
}

public class DateOnlyDto
{
    public int year { get; set; }
    public int month { get; set; }
    public int day { get; set; }
}