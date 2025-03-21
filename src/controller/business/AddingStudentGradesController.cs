using wsmcbl.src.exception;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class AddingStudentGradesController : BaseController
{
    public AddingStudentGradesController(DaoFactory daoFactory) : base(daoFactory)
    {
    }

    public async Task<EnrollmentEntity> getEnrollmentById(string enrollmentId)
    {
        return await daoFactory.enrollmentDao!.getFullById(enrollmentId);
    }

    public async Task<List<EnrollmentEntity>> getEnrollmentListByTeacherId(string teacherId)
    {
        return await daoFactory.enrollmentDao!.getListByTeacherId(teacherId);
    }

    public async Task<List<SubjectPartialEntity>> getSubjectPartialList(SubjectPartialEntity parameter)
    {
        return await daoFactory.subjectPartialDao!.getListBySubject(parameter);
    }

    public async Task addGrades(SubjectPartialEntity parameter, List<GradeEntity> gradeList)
    {
        await daoFactory.gradeDao!.addRange(parameter, gradeList);
        await daoFactory.ExecuteAsync();
    }

    public async Task<List<PartialEntity>> getPartialList()
    {
        return await daoFactory.partialDao!.getListInCurrentSchoolyear();
    }

    public async Task<TeacherEntity> getTeacherById(string teacherId)
    {
        var result = await daoFactory.teacherDao!.getById(teacherId);
        if (result == null)
        {
            throw new EntityNotFoundException("TeacherEntity", teacherId);
        }

        await result.setCurrentEnrollment(daoFactory.schoolyearDao!);

        return result;
    }
}