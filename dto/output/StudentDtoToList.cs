using wsmcbl.back.model.accounting;

namespace wsmcbl.back.dto.output;

public class StudentDtoToList
{
    public string studentId { get;}
    public string fullName { get; }
    public string enrollmentLabel { get; }
    public string schoolyear { get; }
    public string tutor { get; }

    public StudentDtoToList(StudentEntity student)
    {
        studentId = student.studentId;
        fullName = student.fullName();
        enrollmentLabel = student.enrollment;
        schoolyear = student.schoolYear;
        tutor = student.tutor;
    }
}