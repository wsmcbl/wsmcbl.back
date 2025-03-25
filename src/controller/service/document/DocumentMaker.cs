using wsmcbl.src.exception;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.service.document;

public class DocumentMaker(DaoFactory daoFactory) : PdfMaker
{
    public async Task<byte[]> getReportCardByStudent(string studentId, string? userId)
    {
        var student = await daoFactory.academyStudentDao!.getCurrentById(studentId);
        var teacher = await daoFactory.teacherDao!.getByEnrollmentId(student.enrollmentId!);
        if (teacher == null)
        {
            throw new EntityNotFoundException($"Teacher with enrollmentId ({student.enrollmentId}) not found.");
        }
            
        var enrollment = await daoFactory.enrollmentDao!.getById(student.enrollmentId!);
        var schoolyear = await daoFactory.schoolyearDao!.getCurrent();
        var partialList = await daoFactory.partialDao!.getListByEnrollmentId(enrollment!.enrollmentId!);
        
        string? userAlias = null;
        if (userId != null)
        {
            var user = await daoFactory.userDao!.getById(userId);
            userAlias = user.getAlias();
        }
        
        var latexBuilder = new ReportCardLatexBuilder.Builder(resource,$"{resource}/out")
            .withStudent(student)
            .withTeacher(teacher)
            .withUserAlias(userAlias)
            .withDegree(enrollment.label)
            .withPartialList(partialList)
            .withSchoolyear(schoolyear.label)
            .withSubjectAreaList(await daoFactory.subjectAreaDao!.getAll())
            .withSubjectList(await daoFactory.subjectDao!.getByEnrollmentId(student.enrollmentId!))
            .build();
        
        setLatexBuilder(latexBuilder);
        return getPDF();
    }
    
    public async Task<byte[]> getEnrollDocument(string studentId, string userId)
    {
        var user = await daoFactory.userDao!.getById(userId);
        var student = await daoFactory.studentDao!.getFullById(studentId);
        var academyStudent = await daoFactory.academyStudentDao!.getById(studentId);
        var enrollment = await daoFactory.enrollmentDao!.getByStudentId(student.studentId!);
        var schoolyear = await daoFactory.schoolyearDao!.getById(academyStudent!.schoolYear);
        var label = schoolyear != null ? schoolyear.label : "";
        
        var latexBuilder = new EnrollSheetLatexBuilder(resource, $"{resource}/out", student);
        latexBuilder.setGrade(enrollment.label);
        latexBuilder.setAcademyStudent(academyStudent);
        latexBuilder.setUsername(user.getAlias());
        latexBuilder.setSchoolyear(label);
        
        setLatexBuilder(latexBuilder);
        return getPDF();
    }

    public async Task<byte[]> getInvoiceDocument(string transactionId)
    {
        var transaction = await daoFactory.transactionDao!.getById(transactionId);
        
        var cashier = await daoFactory.cashierDao!.getById(transaction!.cashierId);
        if (cashier is null)
        {
            throw new EntityNotFoundException("CashierEntity", transaction.cashierId);
        }
        
        var student = await daoFactory.accountingStudentDao!.getFullById(transaction.studentId);
        var exchangeRate = await daoFactory.exchangeRateDao!.getLastRate();
        var generalBalance = await daoFactory.debtHistoryDao!.getGeneralBalance(transaction.studentId);
        
        var latexBuilder = new InvoiceLatexBuilder
            .Builder(resource, $"{resource}/out")
            .withStudent(student)
            .withTransaction(transaction)
            .withCashier(cashier)
            .withGeneralBalance(generalBalance)
            .withNumber(transaction.number)
            .withSeries("A")
            .withExchangeRate(exchangeRate.value)
            .build();
        
        setLatexBuilder(latexBuilder);
        return getPDF();
    }

    public async Task<byte[]> getOfficialEnrollmentListDocument(string userId)
    {
        var schoolyear = await daoFactory.schoolyearDao!.getCurrentOrNew();
        
        var user = await daoFactory.userDao!.getById(userId);
        var degreeList = await daoFactory.degreeDao!.getListForSchoolyearId(schoolyear.id!, true);
        var teacherList = await daoFactory.teacherDao!.getAll();
        
        var latexBuilder = new OfficialEnrollmentListLatexBuilder.Builder(resource,$"{resource}/out")
            .withDegreeList(degreeList)
            .withTeacherList(teacherList)
            .withUserName(user.getAlias())
            .build();
        
        setLatexBuilder(latexBuilder);
        return getPDF();
    }

    public async Task<byte[]> getDebtorReport(string userId)
    {
        var user = await daoFactory.userDao!.getById(userId);
        var studentList = await daoFactory.accountingStudentDao!.getDebtorStudentList();
        var degreeList = await daoFactory.degreeDao!.getValidListForNewOrCurrentSchoolyear();
        
        var latexBuilder = new DebtorReportLatexBuilder.Builder(resource, $"{resource}/out")
            .withStudentList(studentList)
            .withDegreeList(degreeList)
            .withUserName(user.getAlias())
            .build();
        
        setLatexBuilder(latexBuilder);
        return getPDF();
    }
}