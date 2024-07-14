namespace wsmcbl.src.model.academy;

public class NoteEntity
{
    public string Studentid { get; set; } = null!;

    public string Subjectid { get; set; } = null!;

    public string Enrollmentid { get; set; } = null!;

    public double? Cumulative { get; set; }

    public double? Exam { get; set; }

    public double? Finalscore { get; set; }

    public virtual StudentEntity Student2 { get; set; } = null!;

    public virtual SubjectEntity Subject { get; set; } = null!;
}