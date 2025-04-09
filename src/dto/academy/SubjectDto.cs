using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.academy;

public class SubjectDto
{
    public string subjectId { get; set; }
    public int areaId { get; set; }
    public string name { get; set; }
    public bool isMandatory { get; set; }
    public int semester { get; set; }
    public string initials { get; set; }
    public int number { get; set; }
    
    public SubjectDto(SubjectEntity value)
    {
        subjectId = value.subjectId!;
        areaId = value.areaId;
        name = value.name;
        isMandatory = value.isMandatory;
        semester = value.semester;
        initials = value.initials;
        number = value.number;
    }    
}