using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.academy;

public class GradesToAddDto
{
    public TeacherEnrollmentDto teacherEnrollment { get; set; } = null!;
    public List<GradeDto> gradeList { get; set; } = null!;

    public List<GradeEntity> getGradeList()
    {
        return gradeList.toEntity();
    }

    public SubjectPartialEntity getSubjectPartial()
    {
        return teacherEnrollment.toEntity();
    }
}