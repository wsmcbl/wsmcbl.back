using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.academy;

public class EnrollmentGuideDto
{
    public EnrollmentDto enrollment { get; set; }
    public List<SubjectDto> subjectList { get; set; }

    public EnrollmentGuideDto(EnrollmentEntity? enrollment)
    {
        subjectList = [];
        
        if (enrollment == null)
        {
            this.enrollment = new EnrollmentDto();
            return;
        }
        
        this.enrollment = new EnrollmentDto(enrollment);
        foreach (var item in enrollment.subjectList!)
        {
            subjectList.Add(new SubjectDto(item.secretarySubject!));
        }
    }
}