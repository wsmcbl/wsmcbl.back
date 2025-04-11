using wsmcbl.src.controller.service.sheet;
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

    public async Task<List<SubjectPartialEntity>> getSubjectPartialList(SubjectPartialEntity subjectPartial)
    {
        return await daoFactory.subjectPartialDao!.getListBySubject(subjectPartial);
    }

    public async Task addGrades(SubjectPartialEntity subjectPartial, List<GradeEntity> gradeList)
    {
        await daoFactory.gradeDao!.addRange(subjectPartial, gradeList);
        await daoFactory.ExecuteAsync();
    }

    public async Task<bool> recordIsNotActive(int partialId)
    {
        var partial = await daoFactory.partialDao!.getById(partialId);
        if (partial == null)
        {
            throw new EntityNotFoundException("PartialEntity", partialId.ToString());
        }

        return !partial.recordIsActive();
    }
    
    public async Task<List<PartialEntity>> getPartialList()
    {
        return await daoFactory.partialDao!.getListForCurrentSchoolyear();
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

    public async Task<byte[]> getEnrollmentToAddGradesDocument(SubjectPartialEntity subjectPartial, string userId)
    {
        var sheetMaker = new SpreadSheetMaker(daoFactory);
        return await sheetMaker.getSubjectGrades(subjectPartial, userId);
    }
}