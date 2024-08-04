using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.academy;

public class StudentScoreInformationDto
{
    public string studentName { get; set; }
    public string teacherName { get; set; }
    public string enrollment { get; set; }
    public List<PartialInformationDto> partials { get; set; }

    public StudentScoreInformationDto(StudentEntity student, TeacherEntity teacher)
    {
        studentName = student.fullName();
        teacherName = teacher.fullName();
        enrollment = teacher.getEnrollmentLabel();

        partials =
        [
            new PartialInformationDto("I parcial", true),
            new PartialInformationDto("II parcial", true),
            new PartialInformationDto("III parcial", false),
            new PartialInformationDto("VI parcial", false)
        ];
    }
}