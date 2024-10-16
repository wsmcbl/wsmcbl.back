using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class AddingStudentGradesController : BaseController, IAddingStudentGradesController
{
    public AddingStudentGradesController(DaoFactory daoFactory) : base(daoFactory)
    {
        
    }
    
    public async Task<List<EnrollmentEntity>> getEnrollmentListByTeacherId(string teacherId)
    {
        return await daoFactory.enrollmentDao!.getListByTeacherId();
    }

    public async Task<List<SubjectEntity>> getEnrollmentByTeacher(string teacherId, string enrollmentId)
    {
        return await daoFactory.subjectDao!.getListByTeacherId(teacherId, enrollmentId);
    }

    public async Task addGrades(string teacherId, List<GradeEntity> grades)
    {
        await daoFactory.gradeDao!.addingStudentGrades(teacherId, grades);
    }

    public async Task<List<PartialEntity>> getPartialList()
    {
        return await daoFactory.partialDao!.getListByCurrentSchoolyear();
    }
}