namespace wsmcbl.src.model.secretary;

public class DegreeDataEntity
{
    public int degreeDataId { get; set; }
    public string label { get; set; } = null!;
    public string tag { get; set; } = null!;
    public int educationalLevel { get; set; }

    public ICollection<SubjectDataEntity>? subjectList { get; set; }
    
    private static readonly List<string> educationalLevels = ["Preescolar", "Primaria", "Secundaria"];
    
    public string getModalityName()
    {
        return educationalLevels[educationalLevel-1];
    }

    public static string getLevelName(int level)
    {
        return level switch
        {
            < 1 => educationalLevels[0],
            > 3 => educationalLevels[2],
            _ => educationalLevels[level - 1]
        };
    }
}