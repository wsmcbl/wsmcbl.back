using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.academy;

public class BasicGuideStudentDto
{
    public string studentId { get; set; }
    public string minedId { get; set; }
    public string fullName { get; set; }
    public string age { get; set; }
    public bool sex { get; set; }

    public BasicGuideStudentDto(StudentEntity value)
    {
        studentId = value.studentId!;
        minedId = value.student.minedId!;
        fullName = value.fullName();
        age = value.student.getAge();
        sex = value.student.sex;
    }
}