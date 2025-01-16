using wsmcbl.src.model.academy;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.academy;

public class EnrollmentListDto
{
    public List<EnrollmentDto> enrollmentList { get; set; } = null!;
    public List<BasicSubjectDto> subjectList { get; set; }

    public EnrollmentListDto(DegreeEntity degree)
    {
        createEnrollmentList(degree.enrollmentList);
        subjectList = degree.subjectList.mapListToBasicDto();
    }

    private void createEnrollmentList(ICollection<EnrollmentEntity>? list)
    {
        if (list == null)
        {
            enrollmentList = [];
            return;
        }

        enrollmentList = list.Select(e => new EnrollmentDto(e)).ToList();
    }
}