namespace wsmcbl.src.model.secretary;

public class SubjectEntity
{
    public string? subjectId { get; set; }
    public string? degreeId { get; set; }
    public string name { get; set; } = null!;
    public bool isMandatory { get; set; }
    public int semester { get; set; }
    public string initials { get; set; } = null!;

    public SubjectEntity()
    {
    }
    
    public SubjectEntity(SubjectDataEntity subjectData)
    {
        name = subjectData.name;
        isMandatory = subjectData.isMandatory;
        semester = subjectData.semester;
        initials = subjectData.initials;
    }
}