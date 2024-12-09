using wsmcbl.src.exception;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class MoveTeacherFromSubjectController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    public async Task<List<TeacherEntity>> getTeacherList()
    {
        return await daoFactory.teacherDao!.getAll();
    }
    
    public async Task<TeacherEntity?> getTeacherById(string teacherId)
    {
        return await daoFactory.teacherDao!.getById(teacherId);
    }
    
    public async Task updateTeacherFromSubject(string subjectId, string enrollmentId, string teacherId)
    {
        var subject = await daoFactory.subjectDao!.getBySubjectAndEnrollment(subjectId, enrollmentId);
        if (subject == null)
        {
            throw new EntityNotFoundException("Subject", subjectId);
        }

        if (subject.teacherId == teacherId)
        {
            throw new ConflictException("The teacher is already associated with the subject.");
        }

        subject.teacherId = teacherId;
        await daoFactory.execute();
    }

    public async Task<bool> isThereAnActivePartial()
    {
        var controller = new MoveStudentFromEnrollmentController(daoFactory);
        return await controller.isThereAnActivePartial();
    }
}