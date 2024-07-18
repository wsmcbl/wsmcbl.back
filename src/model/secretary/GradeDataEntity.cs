namespace wsmcbl.src.model.secretary;

public class GradeDataEntity
{
    public int gradeDataId { get; set; }

    public string label { get; set; } = null!;

    public int modality { get; set; }

    public virtual ICollection<SubjectDataEntity> subjectList { get; set; }
}