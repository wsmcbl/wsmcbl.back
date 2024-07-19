namespace wsmcbl.src.model.secretary;

public class SubjectDataEntity
{
    public int subjectDataId { get; set; }
    public int gradeDataId { get; set; }
    public string name { get; set; }
    public bool isMandatory { get; set; }
    public int semester { get; set; }
}