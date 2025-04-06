namespace wsmcbl.src.model.academy;

public class WithdrawnStudentEntity
{
    public int withdrawnId {get; set;}
    public string studentId { get; set; } = null!;
    public string lastEnrollmentId { get; set; } = null!;
    public string schoolyearId { get; set; } = null!;
    public DateTime withdrawnAt { get; set; }
}