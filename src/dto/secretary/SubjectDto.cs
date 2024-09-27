using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.secretary;

public class SubjectDto
{
    public string? subjectId { get; set; }
    public string name { get; set; } = null!;
    public bool isMandatory { get; set; }
    public int semester { get; set; }
    public string initials { get; set; } = null!;
    
    public SubjectDto()
    {
    }

    public SubjectDto(SubjectEntity subject)
    {
        subjectId = subject.subjectId!;
        name = subject.name;
        isMandatory = subject.isMandatory;
        semester = subject.semester;
        initials = subject.initials;
    }
}