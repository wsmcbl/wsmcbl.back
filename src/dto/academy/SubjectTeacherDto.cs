using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.academy;

public class SubjectTeacherDto : SubjectDto
{
    public string enrollmentId {get; set;}
    
    public SubjectTeacherDto(SubjectEntity value) : base(value.secretarySubject!)
    {
        enrollmentId = value.enrollmentId;
    }
}