using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.secretary;

public class WithdrawnStudentDto
{
    public int withdrawnId {get; set;}
    public string studentId { get; set; }
    public string fullName { get; set; }
    public string lastEnrollmentId { get; set; }
    public string lastEnrollmentLabel { get; set; }
    public string schoolyearId { get; set; }
    public DateTime enrolledAt { get; set; }
    public DateTime withdrawnAt { get; set; }
    
    public WithdrawnStudentDto(WithdrawnStudentEntity parameter)
    {
        withdrawnId = parameter.withdrawnId;
        studentId = parameter.studentId;
        fullName = parameter.student!.fullName();
        lastEnrollmentId = parameter.lastEnrollmentId;
        lastEnrollmentLabel = parameter.lastEnrollment!.label;
        schoolyearId = parameter.schoolyearId;
        enrolledAt = parameter.enrolledAt;
        withdrawnAt = parameter.withdrawnAt;
    }
}