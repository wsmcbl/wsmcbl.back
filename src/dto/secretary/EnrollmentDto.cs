using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.secretary;

public class EnrollmentDto
{
    public string enrollmentId { get; set; } = null!;
    public string? teacherId {get; set; }
    public string? teacherName { get; set; }
    public string label { get; set; } = null!;
    public string? section { get; set; }
    public int capacity { get; set; }
    public int quantity { get; set; }
    
    public List<SubjectToAssignDto> subjects { get; set; } = null!;

    public EnrollmentDto()
    {
    }
    
    private EnrollmentDto(EnrollmentEntity entity)
    {
        enrollmentId = entity.enrollmentId!;
        teacherId = entity.teacherId;
        section = entity.section;
        capacity = entity.capacity;
        quantity = entity.quantity;
        label = entity.label;
        subjects = entity.subjectList.mapListToAssignDto();
    }

    public EnrollmentDto(EnrollmentEntity enrollment, TeacherEntity? teacher) : this(enrollment)
    {
        if (teacher == null)
        {
            return;
        }
        
        teacherId = teacher.teacherId;
        teacherName = teacher.fullName();
    }
}