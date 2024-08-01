using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.academy;

public class StudentScoreInformationDto
{
    public string studentName { get; set; }
    public string teacherName { get; set; }
    

    public StudentScoreInformationDto(StudentEntity student, TeacherEntity teacher)
    {
        studentName = student.fullName();
        teacherName = teacher.fullName();
    }
}