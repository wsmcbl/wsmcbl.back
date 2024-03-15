using wsmcbl.back.model.accounting;

namespace wsmcbl.back.dto.output;

public class StudentDto
{
    public string id { get;}
    public string fullName { get; set; }
    public string schoolyear { get; set; }
    public string tutor { get; set; }
    
    public StudentDto(StudentEntity student) : this(student.studentId, student.fullName(), student.schoolYear, student.tutor)
    {
    }
    
    public StudentDto(string id, string fullName, string schoolyear, string tutor)
    {
        this.id = id;
        this.fullName = fullName;
        this.schoolyear = schoolyear;
        this.tutor = tutor;
    }
}