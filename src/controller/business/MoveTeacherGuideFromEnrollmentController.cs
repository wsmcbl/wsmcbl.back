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
    
    public async Task assignTeacherGuide(string newTeacherId, string enrollmentId)
    {
        var oldTeacher = await daoFactory.teacherDao!.getByEnrollmentId(enrollmentId);
        if (oldTeacher != null)
        {
            oldTeacher.deleteEnrollment();
            daoFactory.teacherDao.update(oldTeacher);
            await daoFactory.execute();
        }

        var newTeacher = await getTeacherById(newTeacherId);
        newTeacher.isGuide = true;
        daoFactory.teacherDao.update(newTeacher);
        await daoFactory.execute();
    }
}