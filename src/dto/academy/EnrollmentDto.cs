using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.academy;

public class EnrollmentDto
{
    public string enrollmentId { get; set; } = null!;
    public string? teacherId {get; set; }
    public string? teacherName { get; set; }
    public string label { get; set; } = null!;
    public string? section { get; set; }
    public int capacity { get; set; }
    public int quantity { get; set; }
    
    public List<SubjectTeacherIdsDto> subjects { get; set; } = null!;

    public EnrollmentDto()
    {
    }
    
    public EnrollmentDto(EnrollmentEntity entity)
    {
        enrollmentId = entity.enrollmentId!;
        teacherId = entity.teacherId;
        section = entity.section;
        capacity = entity.capacity;
        quantity = entity.quantity;
        label = entity.label;

        if (entity.subjectList == null || entity.subjectList.Count == 0)
        {
            subjects = [];
        }

        subjects = entity.subjectList!.Select(e => new SubjectTeacherIdsDto(e)).ToList();
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