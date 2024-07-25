namespace wsmcbl.src.model.secretary;

public class StudentTutorEntity
{
    public string tutorId { get; set; } = null!;
    public string studentId { get; set; } = null!;
    public string name { get; set; } = null!;
    public string phone { get; set; } = null!;

    public StudentTutorEntity()
    {
    }

    public StudentTutorEntity(string name, string phone)
    {
        this.name = name;
        this.phone = phone;
    }
}