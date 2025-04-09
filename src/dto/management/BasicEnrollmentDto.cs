using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.management;

public class BasicEnrollmentDto
{
    public string enrollmentId { get; set; }
    public string teacherId { get; set; }
    public string degreeId { get; set; }
    public string label { get; set; }
    public string tag { get; set; }
    
    public BasicEnrollmentDto(EnrollmentEntity parameter)
    {
        enrollmentId = parameter.enrollmentId!;
        teacherId = parameter.teacherId!;
        degreeId = parameter.degreeId;
        label = parameter.label;
        tag = parameter.tag;
    }
}