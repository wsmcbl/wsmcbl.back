using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.academy;

public class BasicSubjectDto
{
    public string subjectId { get; set; }
    public int areaId { get; set; }
    public string name { get; set; }
    public string initials { get; set; }
    public int number { get; set; }

    public BasicSubjectDto(SubjectEntity subject)
    {
        subjectId = subject.subjectId!;
        areaId = subject.areaId;
        name = subject.name;
        initials = subject.initials;
        number = subject.number;
    }
}