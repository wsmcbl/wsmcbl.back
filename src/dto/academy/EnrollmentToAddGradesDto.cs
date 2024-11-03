using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.academy;

public class EnrollmentToAddGradesDto
{
    public string label { get; set; }
    public List<SubjectPartialDto> subjectPartialList { get; set; }
    public List<BasicStudentDto> studentList { get; set; }
    public List<BasicSubjectDto> subjectList { get; set; }
    
    public EnrollmentToAddGradesDto(EnrollmentEntity enrollment, IEnumerable<model.secretary.SubjectEntity> subjectList)
    {
        label = enrollment.label;
        //subjectPartialList = subjectList.mapListToDto();
        
        studentList = enrollment.studentList!.mapListToDto();
        this.subjectList = subjectList.mapListToBasicDto();
    }
}