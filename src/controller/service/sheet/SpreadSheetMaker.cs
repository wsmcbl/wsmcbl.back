using wsmcbl.src.exception;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.service.sheet;

public class SpreadSheetMaker
{
    private DaoFactory daoFactory { get; set; }

    public SpreadSheetMaker(DaoFactory daoFactory)
    {
        this.daoFactory = daoFactory;
    }

    public async Task<byte[]> getStudentRegisterForCurrentSchoolyear(string userId)
    {
        var user = await daoFactory.userDao!.getById(userId);
        var currentSchoolyear = await daoFactory.schoolyearDao!.getCurrentOrNew();
        var registerList = await daoFactory.studentDao!.getStudentRegisterListForCurrentSchoolyear();
        
        var sheetBuilder = new StudentRegisterSheetBuilder.Builder()
            .withUser(user)
            .withSchoolyear(currentSchoolyear)
            .withRegisterList(registerList)
            .build();

        return sheetBuilder.getSpreadSheet();
    }

    public async Task<byte[]> getSubjectGradesByTeacherId(string teacherId, string enrollmentId, int partialId)
    {
        var teacher = await daoFactory.teacherDao!.getById(teacherId);
        if (teacher == null)
        {
            throw new EntityNotFoundException("TeacherEntity", teacherId);
        }

        var enrollment = await daoFactory.enrollmentDao!.getById(enrollmentId);
        if (enrollment == null)
        {
            throw new EntityNotFoundException("EnrollmentEntity", enrollmentId);
        }
        
        var subjectPartial = new SubjectPartialEntity(teacherId, enrollmentId, partialId);
        var subjectPartialList = await daoFactory.subjectPartialDao!.getListBySubject(subjectPartial);

        var sheetBuilder = new SubjectGradesSheetBuilder.Builder()
            .withTeacher(teacher)
            .withEnrollment(enrollment)
            .withSubjectPartialList(subjectPartialList)
            .build();

        return sheetBuilder.getSpreadSheet();
    }
}