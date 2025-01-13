using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.academy;

public class EnrollmentDto
{
    public string enrollmentId { get; set; }
    public string teacherId {get; set; }
    public string label { get; set; }
    public string section { get; set; }
    public int capacity { get; set; }
    public int quantity { get; set; }

    public List<BasicStudentDto> studentList { get; set; } = null!;
    public List<SubjectTeacherIdsDto> subjectList { get; set; } = null!;
    
    public EnrollmentDto(EnrollmentEntity entity)
    {
        enrollmentId = entity.enrollmentId!;
        teacherId = entity.teacherId!;
        section = entity.section;
        capacity = entity.capacity;
        quantity = entity.quantity;
        label = entity.label;
        
        createStudentList(entity.studentList);
        createSubjectList(entity.subjectList);
    }

    private void createSubjectList(ICollection<SubjectEntity>? list)
    {
        if (list == null || list.Count == 0)
        {
            subjectList = [];
            return;
        }

        subjectList = list.Select(e => new SubjectTeacherIdsDto(e)).ToList();
    }

    private void createStudentList(ICollection<StudentEntity>? list)
    {
        if (list == null)
        {
            studentList = [];
            return;
        }

        studentList = list.mapListToDto();
    }
}