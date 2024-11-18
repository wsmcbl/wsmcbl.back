using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.secretary;

public class BasicStudentDto
{
    public string studentId { get; set; }
    public string fullName { get; set; }
    public bool isActive { get; set; }
    public string schoolyear { get; set; }
    public string enrollment { get; set; }
    
    public BasicStudentDto(StudentEntity entity, string schoolyear, string enrollment)
    {
        studentId = entity.studentId!;
        fullName = entity.fullName();
        isActive = entity.isActive;
        this.schoolyear = schoolyear;
        this.enrollment = enrollment;
    }
} 