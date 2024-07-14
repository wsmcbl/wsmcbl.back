using wsmcbl.src.model.secretary;

namespace wsmcbl.src.model.academy;

public class EnrollmentEntity
{
    public string Enrollmentid { get; set; } = null!;

    public string Enrollmentlabel { get; set; } = null!;

    public string Schoolyear { get; set; } = null!;

    public string Section { get; set; } = null!;

    public short? Capacity { get; set; }

    public short? Quantity { get; set; }

    public int Gradeid { get; set; }

    public virtual GradeEntity Grade { get; set; } = null!;

    public virtual SchoolyearEntity SchoolyearNavigation { get; set; } = null!;

    public virtual ICollection<StudentEntity> Student2s { get; set; } = new List<StudentEntity>();

    public virtual ICollection<SubjectEntity> Subjects { get; set; } = new List<SubjectEntity>();

    public virtual ICollection<TeacherEntity> Teachers { get; set; } = new List<TeacherEntity>();
    
    public void assignTeacher(string subjectId, TeacherEntity teacher)
    {
        var subject = Subjects.First(t => t.Subjectid == subjectId);
        subject.Teacher = teacher;
    }
    
    
}