namespace wsmcbl.src.model.academy;

public class StudentEntity
{
    public string Studentid { get; set; } = null!;

    public string Enrollmentid { get; set; } = null!;

    public string Schoolyear { get; set; } = null!;

    public bool? Isapproved { get; set; }

    public virtual EnrollmentEntity Enrollment { get; set; } = null!;

    public virtual ICollection<NoteEntity> Notes { get; set; } = new List<NoteEntity>();

    public virtual secretary.StudentEntity Student { get; set; } = null!;
}