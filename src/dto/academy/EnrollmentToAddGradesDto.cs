using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.academy;

public class EnrollmentToAddGradesDto
{
    public string label { get; set; }
    public List<SubjectPartialDto> subjectList { get; set; }
    public List<BasicStudentDto> studentList { get; set; }
    
    public EnrollmentToAddGradesDto(EnrollmentEntity enrollment, IEnumerable<SubjectEntity> subjectList)
    {
        label = enrollment.label;
        studentList = enrollment.studentList!.mapListToDto();
        this.subjectList = subjectList.mapListToDto();
    }
}