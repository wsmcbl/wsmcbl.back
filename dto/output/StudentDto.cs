using wsmcbl.back.model.entity.accounting;

namespace wsmcbl.back.dto.output;

public class StudentDto
{
    public StudentDto(StudentEntity student) : this(student.getId(), student.fullName(), student.enrollment)
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