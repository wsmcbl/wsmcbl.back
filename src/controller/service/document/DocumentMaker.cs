using wsmcbl.src.exception;
using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;

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

        var latexBuilder = new ReportCardLatexBuilder.Builder(resource, $"{resource}/out/card")
            .withStudent(student)
            .withTeacher(teacher)
            .withUserAlias(userAlias)
            .withDegree(enrollment.label)
            .withPartialList(partialList)
            .withSchoolyear(schoolyear.label)
            .withSubjectAreaList(await daoFactory.subjectAreaDao!.getAll())
            .withSubjectList(await daoFactory.academySubjectDao!.getByEnrollmentId(student.enrollmentId!))
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
        var schoolyear = await daoFactory.schoolyearDao!.getById(academyStudent!.schoolyearId);

        var latexBuilder = new EnrollSheetLatexBuilder.Builder(resource, $"{resource}/out/enroll")
            .withStudent(student)
            .withAcademyStudent(academyStudent)
            .withGrade(enrollment.label)
            .withSchoolyear(schoolyear?.label)
            .withUserAlias(user.getAlias())
            .build();

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
                .Builder(resource, $"{resource}/out/invoice")
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

        var latexBuilder = new OfficialEnrollmentListLatexBuilder
                .Builder(resource, $"{resource}/out/enrollments")
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

        var latexBuilder = new DebtorReportLatexBuilder
                .Builder(resource, $"{resource}/out/debtor")
            .withStudentList(studentList)
            .withDegreeList(degreeList)
            .withUserName(user.getAlias())
            .build();

        setLatexBuilder(latexBuilder);
        return getPDF();
    }

    public async Task<byte[]> getActiveCertificateByStudent(string studentId, string userId)
    {
        var student = await daoFactory.academyStudentDao!.getCurrentById(studentId);
        var enrollment = await daoFactory.enrollmentDao!.getById(student.enrollmentId!);
        var degree = await daoFactory.degreeDao!.getById(enrollment!.degreeId);
        var schoolyear = await daoFactory.schoolyearDao!.getCurrent();
        
        var user = await daoFactory.userDao!.getById(userId);

        var latexBuilder = new ActiveCertificateLatexBuilder.Builder(resource, $"{resource}/out/active")
            .withStudent(student)
            .withUserAlias(user.getAlias())
            .withEnrollment(enrollment.label)
            .withLevel(degree!.educationalLevel)
            .withSchoolyear(schoolyear.label)
            .build();

        setLatexBuilder(latexBuilder);
        return getPDF();
    }

    public async Task<byte[]> getProformaByStudent(string studentId, string userId)
    {
        var student = await daoFactory.academyStudentDao!.getCurrentById(studentId);
        var enrollment = await daoFactory.enrollmentDao!.getById(student.enrollmentId!);
        var degree = await daoFactory.degreeDao!.getById(enrollment!.degreeId);
        var schoolyear = await daoFactory.schoolyearDao!.getCurrent();

        var tariffList = await daoFactory.tariffDao!.getAll();
        tariffList = tariffList
            .Where(e => new DegreeDataEntity().getLevelName(e.educationalLevel) == degree!.educationalLevel)
            .OrderBy(e => e.dueDate).ToList();
        
        var user = await daoFactory.userDao!.getById(userId);

        var latexBuilder = new ProformaLatexBuilder.Builder(resource, $"{resource}/out/proforma")
            .withStudent(student.fullName())
            .withUserAlias(user.getAlias())
            .withDegree(degree!)
            .withSchoolyear(schoolyear.label)
            .withTariffList(tariffList)
            .build();

        setLatexBuilder(latexBuilder);
        return getPDF();
    }

    public Task<byte[]> getProformaByDegree(string degreeId, string name, string userId)
    {
        throw new NotImplementedException();
    }
}