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

        partials = [];
        foreach (var item in student.partials)
        {
            partials.Add(new PartialInformationDto(item.label, item.isClosed()));
        }
    }
}