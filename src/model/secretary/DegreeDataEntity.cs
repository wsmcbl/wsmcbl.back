namespace wsmcbl.src.model.secretary;

public class DegreeDataEntity
{
    public int degreeDataId { get; set; }
    public string label { get; set; } = null!;
    public int educationalLevel { get; set; }
    public string tag { get; set; }

    public ICollection<SubjectDataEntity>? subjectList { get; set; }

    
    private readonly List<string> educationalLevels = ["Preescolar", "Primaria", "Secundaria"];
    public string getModalityName()
    {
        return educationalLevels[educationalLevel-1];
    }
}