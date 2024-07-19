namespace wsmcbl.src.model.secretary;

public class GradeDataEntity
{
    public int gradeDataId { get; set; }
    public string label { get; set; } = null!;
    public int modality { get; set; }

    public ICollection<SubjectDataEntity> subjectList { get; set; }

    
    private readonly List<string> modalities = ["Preescolar", "Primaria", "Secundaria"];
    public string getModalityName()
    {
        return modalities[modality-1];
    }
}