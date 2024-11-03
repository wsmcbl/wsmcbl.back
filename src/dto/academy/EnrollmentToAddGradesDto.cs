using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.academy;

public class EnrollmentToAddGradesDto
{
    public string label { get; set; }
    public List<SubjectPartialDto> subjectList { get; set; }
    public List<BasicStudentDto> studentList { get; set; }

    public EnrollmentToAddGradesDto()
    {
    }
    
    public EnrollmentToAddGradesDto(EnrollmentEntity enrollment)
    {
        label = enrollment.label;
        studentList = enrollment.studentList!.mapListToDto();
    }
}