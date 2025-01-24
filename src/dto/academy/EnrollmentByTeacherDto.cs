using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.academy;

public class EnrollmentByTeacherDto
{
    public string degreeId { get; set; }
    public string enrollmentId { get; set; }
    public string enrollmentLabel { get; set; }
    public int position { get; set; }
    public int studentQuantity { get; set; }
    public int subjectQuantity { get; set; }
    public int numberOfGrades { get; set; }
    
    public EnrollmentByTeacherDto(EnrollmentEntity enrollment, string teacherId)
    {
        degreeId = enrollment.degreeId;
        enrollmentId = enrollment.enrollmentId!;
        enrollmentLabel = enrollment.label;
        position = Convert.ToInt32(enrollment.tag);
        studentQuantity = enrollment.quantity;
        subjectQuantity = enrollment.getSubjectListByTeacher(teacherId).Count;
    }
}