using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.secretary;

public class SubjectDto
{
    public string subjectId { get; set; }
    public string name { get; set; }
    public bool isMandatory { get; set; }
    public int semester { get; set; }
    
    public SubjectDto()
    {
    }

    public SubjectDto(SubjectEntity subject)
    {
        subjectId = subject.subjectId;
        name = subject.name;
        isMandatory = subject.isMandatory;
        semester = subject.semester;
    }
}