using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.secretary;

public class BasicStudentToEnrollDto
{
    public string studentId { get; set; } = null!;
    public string fullName { get; set; } = null!;
    public string schoolyear { get; set; } = null!;

    public BasicStudentToEnrollDto()
    {
    }
    
    public BasicStudentToEnrollDto(StudentEntity student, string schoolyear)
    {
        studentId = student.studentId!;
        fullName = student.fullName();
        this.schoolyear = schoolyear;
    }
}