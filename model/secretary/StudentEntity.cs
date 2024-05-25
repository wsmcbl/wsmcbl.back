namespace wsmcbl.back.model.secretary;

public class StudentEntity
{
    public string studentId { get; set; }
    public string name { get; set; }
    public string? secondName { get; set; }
    public string surname { get; set; }
    public string? secondSurname { get; set; }
    public bool isActive { get; set; }
    public string schoolYear { get; set; }
    public string? tutor { get; set; }
    public bool sex { get; set; }
    public DateOnly birthday { get; set; }
    public string? enrollmentLabel { get; set; }
    public string fullName()
    {
        return name + " " + secondName + " " + surname + " " + secondSurname;
    }
}