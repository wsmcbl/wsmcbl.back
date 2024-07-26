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

    public StudentTutorEntity(string tutorId, string name, string phone)
    {
        this.tutorId = tutorId;
        this.name = name;
        this.phone = phone;
    }

    public void update(StudentTutorEntity entity)
    {
        name = entity.name;
        phone = entity.phone;
    }
}