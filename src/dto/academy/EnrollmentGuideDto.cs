using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.academy;

public class EnrollmentGuideDto
{
    public string? enrollmentId { get; set; }
    public string label { get; set; }
    public string? section { get; set; }
    public int capacity { get; set; }
    public int quantity { get; set; }
    public string? schoolyear { get; set; }

    public List<BasicGuideStudentDto>? studentList { get; set; }
    
    public List<SubjectDto>? subjectList { get; set; }
    
    public List<SubjectTeacherIdsDto>? subjectTeacherIdList { get; set; }

    public EnrollmentGuideDto(EnrollmentEntity? entity)
    {
        subjectList = [];
        
        if (entity == null)
        {
            label = "Sin asignar";
            return;
        }
        
        enrollmentId = entity.enrollmentId!;
        section = entity.section;
        capacity = entity.capacity;
        quantity = entity.quantity;
        label = entity.label;
        schoolyear = entity.schoolYear;
        
        createStudentList(entity.studentList);
        createSubjectList(entity.subjectList);
    }

    private void createSubjectList(ICollection<SubjectEntity>? list)
    {
        if (list == null)
        {
            subjectList = [];
            return;
        }

        subjectList = [];
        foreach (var item in list)
        {
            subjectList.Add(new SubjectDto(item.secretarySubject!));
        }
        
        subjectTeacherIdList = list.Select(e => new SubjectTeacherIdsDto(e)).ToList();
    }

    private void createStudentList(ICollection<StudentEntity>? list)
    {
        if (list == null)
        {
            studentList = [];
            return;
        }
        
        studentList = [];
        foreach (var item in list)
        {
            studentList.Add(new BasicGuideStudentDto(item));
        }
    }
}