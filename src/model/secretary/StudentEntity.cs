namespace wsmcbl.src.model.secretary;

public class StudentEntity
{
    public string? studentId { get; set; }
    public string name { get; set; } = null!;
    public string? secondName { get; set; }
    public string surname { get; set; } = null!;
    public string? secondSurname { get; set; }
    public bool isActive { get; set; }
    public string schoolYear { get; set; } = null!;
    public string? tutor { get; set; }
    public bool sex { get; set; }
    public DateOnly birthday { get; set; }
    public string enrollmentLabel => "Por implementar";

    public string fullName()
    {
        return name + " " + secondName + " " + surname + " " + secondSurname;
    }

    public void init()
    {
        isActive = true;
        schoolYear = DateTime.Now.Year.ToString();
    }
}