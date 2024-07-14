namespace wsmcbl.src.model.secretary;

public class SubjectEntity
{
    public string Subjectid { get; set; } = null!;

    public int Gradeid { get; set; }

    public string Name { get; set; } = null!;

    public virtual GradeEntity Grade { get; set; } = null!;

    public virtual ICollection<academy.SubjectEntity> Subjects { get; set; } = new List<academy.SubjectEntity>();
}