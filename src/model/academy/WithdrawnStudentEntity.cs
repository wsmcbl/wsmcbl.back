namespace wsmcbl.src.model.academy;

public class WithdrawnStudentEntity
{
    public int withdrawnId {get; set;}
    public string studentId { get; set; } = null!;
    public string lastEnrollmentId { get; set; } = null!;
    public string schoolyearId { get; set; } = null!;
    public DateTime enrolledAt { get; set; }
    public DateTime withdrawnAt { get; set; }
    
    public secretary.StudentEntity? student { get; set; }
    public EnrollmentEntity? lastEnrollment { get; set; }

    public WithdrawnStudentEntity()
    {
    }

    public WithdrawnStudentEntity(StudentEntity parameter)
    {
        studentId = parameter.studentId;
        lastEnrollmentId = parameter.enrollmentId!;
        schoolyearId = parameter.schoolyearId;
        enrolledAt = parameter.createdAt;
        withdrawnAt = DateTime.UtcNow;
    }
}