namespace wsmcbl.src.model.academy;

public class SubjectEntity
{
    public string Subjectid { get; set; } = null!;

    public string Basesubjectid { get; set; } = null!;

    public string Teacherid { get; set; } = null!;

    public string Enrollmentid { get; set; } = null!;

    public virtual secretary.SubjectEntity Basesubject { get; set; } = null!;

    public virtual EnrollmentEntity Enrollment { get; set; } = null!;

    public virtual ICollection<NoteEntity> Notes { get; set; } = new List<NoteEntity>();

    public virtual TeacherEntity Teacher { get; set; } = null!;
}