using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.academy;

public class EnrollmentGuideDto
{
    public EnrollmentDto enrollment { get; set; }
    public List<SubjectDto> subjectList { get; set; }

    public EnrollmentGuideDto(EnrollmentEntity enrollment, List<model.secretary.SubjectEntity> subjectList)
    {
        this.enrollment = new EnrollmentDto(enrollment);

        this.subjectList = [];
        foreach (var item in subjectList)
        {
            this.subjectList.Add(new SubjectDto(item));
        }
    }
}