using wsmcbl.src.dto.accounting;

namespace wsmcbl.src.dto.academy;

public class EnrollmentSubjectDto
{
    public List<SubjectPartialDto> subjects { get; set; }
    public List<BasicStudentDto> studentList { get; set; }
}