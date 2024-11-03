using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class AddingStudentGradesController : BaseController, IAddingStudentGradesController
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

    public async Task<List<SubjectPartialEntity>> getSubjectPartialList(string enrollmentId, string teacherId)
    {
        return await daoFactory.subjectPartialDao!.getByTeacherAndEnrollment(teacherId, enrollmentId);
    }

    public async Task addGrades(string teacherId, List<GradeEntity> grades)
    {
        await daoFactory.gradeDao!.addingStudentGrades(teacherId, grades);
        await daoFactory.execute();
    }

    public async Task<List<PartialEntity>> getPartialList()
    {
        return await daoFactory.partialDao!.getListByCurrentSchoolyear();
    }
}