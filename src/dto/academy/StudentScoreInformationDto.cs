using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.academy;

public class StudentScoreInformationDto
{
    public string studentId { get; set; }
    public string studentName { get; set; }
    public string teacherName { get; set; }
    public string enrollment { get; set; }
    public bool hasSolvency { get; set; }

    public StudentScoreInformationDto(StudentEntity student, TeacherEntity teacher)
    {
        studentId = student.studentId;
        studentName = student.fullName();
        teacherName = teacher.fullName();
        enrollment = teacher.getEnrollmentLabel();
    }

    public void setSolvency(bool parameter)
    {
        hasSolvency = parameter;
    }
}