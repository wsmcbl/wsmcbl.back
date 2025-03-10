using wsmcbl.src.model.config;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.model.academy;

public class TeacherEntity
{
    public Guid userId { get; set; }
    public string? teacherId { get; set; }
    public bool isGuide { get; set; }
    
    public UserEntity user { get; set; } = null!;
    public EnrollmentEntity? enrollment { get; set; }
    public List<EnrollmentEntity>? enrollmentList { get; set; }

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

    public string getEnrollmentLabel()
    {
        return enrollment!.label;
    }

    public void forgetEnrollment()
    {
        enrollment = null;
        isGuide = false;
    }

    public async Task<EnrollmentEntity?> getCurrentEnrollment(ISchoolyearDao schoolyearDao)
    {
        if (enrollmentList == null)
        {
            return null;
        }
        
        var current = await schoolyearDao.getCurrentOrNew();

        return enrollmentList.FirstOrDefault(e => e.schoolYear == current.id);
    }
}