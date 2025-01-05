using wsmcbl.src.model.accounting;

namespace wsmcbl.src.dto.secretary;

public class StudentWithLevelDto
{
    public string studentId { get; set; }
    public string fullName { get; set; }
    public int educationalLevel { get; set; }

    public StudentWithLevelDto(StudentEntity student)
    {
        studentId = student.studentId!;
        fullName = student.fullName();
        educationalLevel = student.educationalLevel;
    }
}