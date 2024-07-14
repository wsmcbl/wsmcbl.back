namespace wsmcbl.src.model.secretary;

public class SubjectEntity
{
    public string subjectId { get; set; } = null!;

    public int gradeId { get; set; }

    public string name { get; set; } = null!;

    public ICollection<academy.SubjectEntity> subjects { get; set; } = new List<academy.SubjectEntity>();
}