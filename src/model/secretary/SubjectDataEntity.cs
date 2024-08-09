namespace wsmcbl.src.model.secretary;

public class SubjectDataEntity
{
    public int subjectDataId { get; set; }
    public int degreeDataId { get; set; }
    public string name { get; set; }
    public bool isMandatory { get; set; }
    public int semester { get; set; }
    public string initials { get; set; }
}