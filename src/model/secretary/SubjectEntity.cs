namespace wsmcbl.src.model.secretary;

public class SubjectEntity
{
    public string subjectId { get; set; } = null!;
    public int gradeId { get; set; }
    public string name { get; set; } = null!;
    
    public ICollection<academy.SubjectEntity> academySubjectList { get; set; } = null!;
}