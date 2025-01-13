using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.academy;

public class SubjectTeacherIdsDto
{
    public string subjectId { get; set; }
    public string teacherId { get; set; }

    public SubjectTeacherIdsDto(SubjectEntity subject)
    {
        subjectId = subject.subjectId;
        teacherId = subject.teacherId!;
    }
}