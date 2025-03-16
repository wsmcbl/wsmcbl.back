using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.secretary;

public class SubjectDto
{
    public int areaId { get; set; }
    public string name { get; set; } = null!;
    public bool isMandatory { get; set; }
    public int semester { get; set; }
    public string initials { get; set; } = null!;
    public int number { get; set; }

    public SubjectDto()
    {
    }

    public SubjectDto(SubjectEntity subject)
    {
        name = subject.name;
        areaId = subject.areaId;
        isMandatory = subject.isMandatory;
        semester = subject.semester;
        initials = subject.initials;
        number = subject.number;
    }
}