using wsmcbl.src.exception;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class MoveTeacherGuideFromEnrollmentController : BaseController, IMoveTeacherGuideFromEnrollmentController
{
    private CreateOfficialEnrollmentController controller;

    public MoveTeacherGuideFromEnrollmentController(DaoFactory daoFactory) : base(daoFactory)
    {
        controller = new CreateOfficialEnrollmentController(daoFactory);
    }

    public async Task<List<TeacherEntity>> getTeacherList()
    {
        var list = await controller.getTeacherList();
        return list.Where(e => !e.isGuide).ToList();
    }

    public async Task<EnrollmentEntity> getEnrollment(string enrollmentId)
    {
        var enrollment = await daoFactory.enrollmentDao!.getById(enrollmentId);

        if (enrollment == null)
        {
            throw new EntityNotFoundException("Enrollment", enrollmentId);
        }

        return enrollment;
    }

    public async Task<TeacherEntity> getTeacherById(string teacherId)
    {
        var result = await controller.getTeacherById(teacherId);

        if (result == null)
        {
            throw new EntityNotFoundException("Teacher", teacherId);
        }

        return result;
    }
    
    public async Task assignTeacherGuide(string teacherId, string enrollmentId)
    {
        var oldTeacher = await daoFactory.teacherDao!.getByEnrollmentId(enrollmentId);
        if (oldTeacher != null)
        {
            oldTeacher.deleteEnrollment();
            daoFactory.teacherDao.update(oldTeacher);
            await daoFactory.execute();
        }
        
        var newTeacher = await daoFactory.teacherDao!.getById(teacherId);
        if (newTeacher == null)
        {
            throw new EntityNotFoundException("Teacher", teacherId);
        }
        
        newTeacher.enrollmentId = enrollmentId;
        newTeacher.isGuide = true;
        daoFactory.teacherDao.update(newTeacher);
        await daoFactory.execute();
    }
}