using wsmcbl.src.exception;

namespace wsmcbl.src.model.academy;

public class EnrollmentEntity
{
    public string enrollmentId { get; set; } = null!;
    public string label { get; set; } = null!;
    public string schoolYear { get; set; } = null!;
    public string section { get; set; } = null!;
    public int capacity { get; set; }
    public int quantity { get; set; }
    public int gradeId { get; set; }

    public ICollection<StudentEntity> students { get; set; }

    public ICollection<SubjectEntity> subjects { get; set; }

    public ICollection<TeacherEntity> teachers { get; set; }

    public void assignTeacher(string subjectId, string teacherId)
    {
        var teacher = teachers.First(e => e.teacherId == teacherId);
        var subject = subjects.First(e => e.subjectId == subjectId);
        
        teacher.subjects.Add(subject);
    }

    public void updateData(string label, string schoolYear, string section, int capacity)
    {
        this.label = label;
        this.schoolYear = schoolYear;
        this.section = section;
        this.capacity = capacity;
    }

    public async Task subjectExistOrSet(string subjectId, ISubjectDao dao)
    {
        var subject = subjects.FirstOrDefault(e => e.subjectId == subjectId);

        if (subject != null)
        {
            return;
        }

        subject = await dao.getById(subjectId);

        if (subject == null)
        {
            throw new EntityNotFoundException("subject", subjectId);
        }
        
        subjects.Add(subject);
    }

    public async Task teacherExistOrSet(string teacherId, ITeacherDao dao)
    {
        var teacher = teachers.FirstOrDefault(e => e.teacherId == teacherId);

        if (teacher != null)
        {
            return;
        }

        teacher = await dao.getById(teacherId);

        if (teacher == null)
        {
            throw new EntityNotFoundException("teacher", teacherId);
        }
        
        teachers.Add(teacher);
    }
}