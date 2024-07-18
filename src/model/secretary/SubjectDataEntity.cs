namespace wsmcbl.src.model.secretary;

public class SubjectDataEntity
{
    public int subjectDataId { get; set; }

    public int gradeDataId { get; set; }

    public string name { get; set; } = null!;

    public bool isMandatory { get; set; }
}