using wsmcbl.src.exception;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class MoveTeacherGuideFromEnrollmentController : BaseController
{
    private readonly MoveTeacherFromSubjectController controller;

    public MoveTeacherGuideFromEnrollmentController(DaoFactory daoFactory) : base(daoFactory)
    {
        controller = new MoveTeacherFromSubjectController(daoFactory);
    }

    public async Task<List<TeacherEntity>> getTeacherList()
    {
        var list = await controller.getTeacherList();
        return list.Where(e => !e.isGuide).ToList();
    }

    public async Task<EnrollmentEntity> getEnrollmentById(string enrollmentId)
    {
        var enrollment = await daoFactory.enrollmentDao!.getById(enrollmentId);
        if (enrollment == null)
        {
            throw new EntityNotFoundException("EnrollmentEntity", enrollmentId);
        }

        return enrollment;
    }

    public async Task<TeacherEntity> getTeacherById(string teacherId)
    {
        var result = await controller.getTeacherById(teacherId);
        if (result == null)
        {
            throw new EntityNotFoundException("TeacherEntity", teacherId);
        }

        return result;
    }
    
    public async Task assignTeacherGuide(TeacherEntity teacher, EnrollmentEntity enrollment)
    {
        var oldTeacher = await daoFactory.teacherDao!.getByEnrollmentId(enrollment.enrollmentId!);
        if (oldTeacher != null)
        {
            oldTeacher.forgetEnrollment();
            daoFactory.teacherDao.update(oldTeacher);
            await daoFactory.ExecuteAsync();
        }

        teacher.isGuide = true;
        daoFactory.teacherDao.update(teacher);
        
        enrollment.teacherId = teacher.teacherId;
        daoFactory.enrollmentDao!.update(enrollment);
        
        await daoFactory.ExecuteAsync();
    }
}