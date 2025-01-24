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

    public async Task<List<SubjectPartialEntity>> getSubjectPartialList(SubjectPartialEntity baseSubjectPartial)
    {
        return await daoFactory.subjectPartialDao!.getListByTeacherAndEnrollment(baseSubjectPartial);
    }

    public async Task addGrades(SubjectPartialEntity baseSubjectPartial, List<GradeEntity> gradeList)
    {
        await daoFactory.gradeDao!.addRange(baseSubjectPartial, gradeList);
        await daoFactory.execute();
    }

    public async Task<List<PartialEntity>> getPartialList()
    {
        return await daoFactory.partialDao!.getListByCurrentSchoolyear();
    }

    public async Task<TeacherEntity> getTeacherById(string teacherId)
    {
        var result = await daoFactory.teacherDao!.getById(teacherId);
        if (result == null)
        {
            throw new EntityNotFoundException("TeacherEntity", teacherId);
        }

        return result;
    }
}