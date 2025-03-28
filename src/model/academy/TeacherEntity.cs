using wsmcbl.src.model.config;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.model.academy;

public class TeacherEntity
{
    public Guid userId { get; set; }
    public string? teacherId { get; set; }
    public bool isGuide { get; set; }

    public UserEntity user { get; set; } = null!;

    public List<EnrollmentEntity>? enrollmentList { get; set; }
    
    public List<SubjectGradedView>? subjectGradedList { get; set; }

    public TeacherEntity()
    {
    }

    public TeacherEntity(Guid userId)
    {
        this.userId = userId;
        isGuide = false;
    }

    public string fullName()
    {
        return user.fullName();
    }

    private EnrollmentEntity? enrollment { get; set; }

    public bool hasCurrentEnrollment()
    {
        return enrollment != null;
    }

    public string getCurrentEnrollmentId()
    {
        return enrollment!.enrollmentId!;
    }

    public string getEnrollmentLabel()
    {
        return enrollment!.label;
    }

    public void forgetEnrollment()
    {
        enrollment = null;
        isGuide = false;
    }

    public async Task setCurrentEnrollment(ISchoolyearDao schoolyearDao)
    {
        if (enrollmentList == null)
        {
            enrollment = null;
            return;
        }

        var current = await schoolyearDao.getCurrentOrNew();
        enrollment = enrollmentList.FirstOrDefault(e => e.schoolYear == current.id);
    }

    public bool hasSubmittedGrades()
    {
        return subjectGradedList != null && subjectGradedList.All(e => e.areAllStudentsGraded());
    }
}