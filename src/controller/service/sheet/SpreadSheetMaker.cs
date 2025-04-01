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

    public async Task<byte[]> getSubjectGrades(SubjectPartialEntity subjectPartial, string userId)
    {
        var user = await daoFactory.userDao!.getById(userId);
        if (user == null)
        {
            throw new EntityNotFoundException("UserEntity", userId);
        }
        
        var teacher = await daoFactory.teacherDao!.getById(subjectPartial.teacherId);
        if (teacher == null)
        {
            throw new EntityNotFoundException("TeacherEntity", subjectPartial.teacherId);
        }

        var enrollment = await daoFactory.enrollmentDao!.getFullById(subjectPartial.enrollmentId);
        if (enrollment == null)
        {
            throw new EntityNotFoundException("EnrollmentEntity", subjectPartial.enrollmentId);
        }
        
        var subjectPartialList = await daoFactory.subjectPartialDao!.getListBySubject(subjectPartial);
        
        var currentSchoolyear = await daoFactory.schoolyearDao!.getCurrentOrNew();

        var sheetBuilder = new SubjectGradesSheetBuilder.Builder()
            .withUserAlias(user.getAlias())
            .withSchoolyear(currentSchoolyear.label)
            .withTeacher(teacher)
            .withEnrollment(enrollment)
            .withSubjectPartialList(subjectPartialList)
            .build();

        return sheetBuilder.getSpreadSheet();
    }

    public async Task<byte[]> getEnrollmentGradeSummary(string enrollmentId, int partialId, string userId)
    {
        var user = await daoFactory.userDao!.getById(userId);
        var enrollment = await daoFactory.enrollmentDao!.getFullById(enrollmentId);
        var studentList = await daoFactory.academyStudentDao!.getListWithGradesForCurrentSchoolyear(enrollmentId, partialId);
        
        var sheetBuilder = new EnrollmentGradeSummarySheetBuilder.Builder()
            .withUser(user)
            .withEnrollment(enrollment)
            .withStudentList(studentList)
            .build();

        return sheetBuilder.getSpreadSheet();
    }
}