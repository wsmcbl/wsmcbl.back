using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.academy;

public class BasicSubjectDto
{
    public string subjectId { get; set; }
    public string name { get; set; }

    public BasicSubjectDto(SubjectEntity subject)
    {
        subjectId = subject.subjectId!;
        name = subject.name;
    }
}