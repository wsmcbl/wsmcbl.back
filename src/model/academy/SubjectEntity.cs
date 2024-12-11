namespace wsmcbl.src.model.academy;

public class SubjectEntity
{
    public string subjectId { get; set; } = null!;
    public string? teacherId { get; set; }
    public string enrollmentId { get; set; } = null!;

    public secretary.SubjectEntity? secretarySubject { get; set; }

    public string getInitials => secretarySubject!.initials;
    
}