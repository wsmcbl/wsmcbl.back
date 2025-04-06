using wsmcbl.src.exception;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.service.sheet;

public class SpreadSheetMaker
{
    private DaoFactory daoFactory { get; set; }
    private SheetBuilder sheetBuilder { get; set; } = null!;

    public SpreadSheetMaker(DaoFactory daoFactory)
    {
        this.daoFactory = daoFactory;
    }

    public async Task<byte[]> getStudentRegisterForCurrentSchoolyear(string userId)
    {
        var user = await daoFactory.userDao!.getById(userId);
        var currentSchoolyear = await daoFactory.schoolyearDao!.getCurrentOrNew();
        var registerList = await daoFactory.studentDao!.getStudentRegisterListForCurrentSchoolyear();
        
        sheetBuilder = new StudentRegisterSheetBuilder.Builder()
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

        sheetBuilder = new SubjectGradesSheetBuilder.Builder()
            .withUserAlias(user.getAlias())
            .withSchoolyear(currentSchoolyear.label)
            .withTeacher(teacher)
            .withEnrollment(enrollment)
            .withSubjectPartialList(subjectPartialList)
            .build();

        return sheetBuilder.getSpreadSheet();
    }

    public async Task<byte[]> getEnrollmentGradeSummary(string enrollmentId, int partial, string userId)
    {
        var schoolyear = await daoFactory.schoolyearDao!.getCurrent();
        var teacher = await daoFactory.teacherDao!.getByEnrollmentId(enrollmentId);
        
        var user = await daoFactory.userDao!.getById(userId);
        var enrollment = await daoFactory.enrollmentDao!.getById(enrollmentId);
        var subjectList = await daoFactory.academySubjectDao!.getByEnrollmentId(enrollmentId, partial);
        var studentList = await daoFactory.academyStudentDao!.getListWithGradesForCurrentSchoolyear(enrollmentId, partial);
        
        sheetBuilder = new EnrollmentGradeSummarySheetBuilder.Builder()
            .withPartial(partial)
            .withSchoolyear(schoolyear.label)
            .withTeacher(teacher!)
            .withUserAlias(user.getAlias())
            .withEnrollment(enrollment!.label)
            .withSubjectList(subjectList)
            .withStudentList(studentList)
            .build();

        return sheetBuilder.getSpreadSheet();
    }

    public async Task<byte[]> getEvaluationStatisticsByTeacher(string enrollmentId, int partialId, string userId)
    {
        var schoolyear = await daoFactory.schoolyearDao!.getCurrent();
        var teacher = await daoFactory.teacherDao!.getByEnrollmentId(enrollmentId);
        
        var partial = await daoFactory.partialDao!.getById(partialId);
        if (partial == null)
        {
            throw new EntityNotFoundException("PartialEntity", partialId.ToString());
        }
        
        var user = await daoFactory.userDao!.getById(userId);
        var enrollment = await daoFactory.enrollmentDao!.getById(enrollmentId);
        var subjectList = await daoFactory.academySubjectDao!.getByEnrollmentId(enrollmentId, partial.semester);
        
        var subjectPartialList = await daoFactory.subjectPartialDao!
            .getListByPartialIdAndEnrollmentId(partialId, enrollmentId);
        
        var studentList = await daoFactory.academyStudentDao!.getListWithGradesForCurrentSchoolyear(enrollmentId, partialId);

        var withdrawnStudentList = await getListBeforeFirstPartial(enrollmentId);
        
        sheetBuilder = new EvaluationStatisticsByTeacherSheetBuilder.Builder()
            .withPartial(partial)
            .withSchoolyear(schoolyear.label)
            .withTeacher(teacher!)
            .withUserAlias(user.getAlias())
            .withEnrollment(enrollment!.label)
            .withSubjectList(subjectList)
            .withSubjectPartialList(subjectPartialList)
            .withStudentList(studentList)
            .withInitialStudentList(withdrawnStudentList)
            .build();

        return sheetBuilder.getSpreadSheet();
    }
    
    private async Task<List<model.secretary.StudentEntity>> getListBeforeFirstPartial(string enrollmentId)
    {
        var result = await daoFactory.academyStudentDao!.getListBeforeFirstPartial(enrollmentId);
        var initialList = result.Select(e => e.student).ToList();
        
        var list = await daoFactory.withdrawnStudentDao!.getListByEnrollmentId(enrollmentId, true);
        var withdrawnStudentList = list.Select(e => e.student!).ToList();
        
        return initialList.Union(withdrawnStudentList).ToList();
    }
}