using wsmcbl.src.exception;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class ViewEnrollmentGuideController : BaseController
{
    public ViewEnrollmentGuideController(DaoFactory daoFactory) : base(daoFactory)
    {
    }
    
    public async Task<EnrollmentEntity?> getEnrollmentGuideByTeacherId(string teacherId)
    {
        var teacher = await daoFactory.teacherDao!.getById(teacherId);
        if (teacher == null)
        {
            throw new EntityNotFoundException("TeacherEntity", teacherId);
        }

        var teacherEnrollment = await teacher.getCurrentEnrollment(daoFactory.schoolyearDao!);
        if (teacherEnrollment == null)
        {
            return null;
        }
        
        var enrollment = await daoFactory.enrollmentDao!.getFullById(teacherEnrollment.enrollmentId!);
        if (enrollment == null)
        {
            throw new EntityNotFoundException($"Entity of type (EnrollmentEntity) with teacher id ({teacherId}) not found.");
        }

        return enrollment;
    }
}