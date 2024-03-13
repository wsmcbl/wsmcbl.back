using wsmcbl.back.model.entity.academy;

namespace wsmcbl.back.dto.output;

public class StudentDto
{
    public StudentDto(StudentEntity student) : this(student.id, student.name, student.surname)
    {
    }
    
    public StudentDto(string id, string fullName, string enrollment)
    {
        this.id = id;
        this.fullName = fullName;
        this.enrollment = enrollment;
    }

    public string id { get;}
    public string fullName { get; set; }
    public string enrollment { get; set; }
}