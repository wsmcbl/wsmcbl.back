using wsmcbl.src.model;

namespace wsmcbl.src.dto.secretary;

public class BasicStudentDto
{
    public string studentId { get; set; }
    public string fullName { get; set; }
    public bool isActive { get; set; }
    public string schoolyear { get; set; }
    public string enrollment { get; set; }

    public BasicStudentDto(StudentView value)
    {
        value.initLabels();
        
        studentId = value.studentId;
        fullName = value.fullName;
        isActive = value.isActive;
        schoolyear = value.schoolyear!;
        enrollment = value.enrollment!;
    }
}