using wsmcbl.src.model.academy;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.academy;

public class EnrollmentListDto
{
    public List<EnrollmentDto> enrollmentList { get; set; } = null!;
    public List<BasicTeacherDto> teacherList { get; set; } = null!;
    public List<BasicStudentDto> subjectList { get; set; } = null!;

    public EnrollmentListDto(DegreeEntity degree, List<TeacherEntity> teacherList)
    {
        
    }
}