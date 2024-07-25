using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.output;

public class StudentBasicToEnrollDto
{
    public string studentId { get; set; } = null!;
    public string fullName { get; set; } = null!;
    public string schoolyear { get; set; } = null!;

    public StudentBasicToEnrollDto(StudentEntity student)
    {
        studentId = student.studentId;
        fullName = student.fullName();
        schoolyear = student.schoolYear;
    }
}