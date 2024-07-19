namespace wsmcbl.src.model.secretary;

public class SubjectEntity
{
    public string subjectId { get; set; } = null!;
    public string gradeId { get; set; }
    public string name { get; set; }
    public bool isMandatory { get; set; }

    public SubjectEntity()
    {
    }
    
    public SubjectEntity(SubjectDataEntity subjectData)
    {
        name = subjectData.name;
        isMandatory = subjectData.isMandatory;
    }
}