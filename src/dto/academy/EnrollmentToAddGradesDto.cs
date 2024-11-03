using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.academy;

public class EnrollmentToAddGradesDto
{
    public string label { get; set; }
    public List<BasicStudentDto> studentList { get; set; }
    public List<BasicSubjectDto> subjectList { get; set; }
    public List<SubjectPartialDto> subjectPartialList { get; set; }
    
    public EnrollmentToAddGradesDto(EnrollmentEntity enrollment, IEnumerable<SubjectPartialEntity> subjectPartialList)
    {
        label = enrollment.label;
        studentList = enrollment.studentList!.mapListToDto();
        subjectList = enrollment.subjectList!.mapListToBasicDto();
        this.subjectPartialList = subjectPartialList.mapListToDto();
    }
}