namespace wsmcbl.src.model.secretary;

public class StudentTutorEntity
{
    public string? tutorId { get; set; }
    public string name { get; set; } = null!;
    public string phone { get; set; } = null!;
    public string? email { get; set; }

    public StudentTutorEntity()
    {
    }
    
    public StudentTutorEntity(string name, string phone, string? tutorId = null, string? email = null)
    {
        this.tutorId = tutorId;
        this.name = name;
        this.phone = phone;
        this.email = email;
    }

    public void update(StudentTutorEntity entity)
    {
        name = entity.name;
        phone = entity.phone;
        email = entity.email;
    }
}