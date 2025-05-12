using wsmcbl.src.exception;
using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.service.document;

public class DocumentMaker(DaoFactory daoFactory) : PdfMaker
{
    public async Task<string> getUserAlias(string userId) => (await daoFactory.userDao!.getById(userId)).getAlias();

    public async Task<byte[]> getReportCardByStudent(string studentId, string? userAlias)
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

    public async Task<byte[]> getEnrollDocument(string studentId, string userAlias)
    {
        var student = await daoFactory.studentDao!.getFullById(studentId);
        var academyStudent = await daoFactory.academyStudentDao!.getById(studentId);
        var enrollment = await daoFactory.enrollmentDao!.getByStudentId(student.studentId!);
        var schoolyear = await daoFactory.schoolyearDao!.getById(academyStudent!.schoolyearId);

        var latexBuilder = new EnrollSheetLatexBuilder.Builder(resource, $"{resource}/out/enroll")
            .withStudent(student)
            .withAcademyStudent(academyStudent)
            .withGrade(enrollment.label)
            .withSchoolyear(schoolyear?.label)
            .withUserAlias(userAlias)
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

        var latexBuilder = new InvoiceLatexBuilder.Builder(resource, $"{resource}/out/invoice")
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

    public async Task<byte[]> getOfficialEnrollmentListDocument(string userAlias)
    {
        var schoolyear = await daoFactory.schoolyearDao!.getCurrentOrNew();

        var degreeList = await daoFactory.degreeDao!.getListForSchoolyearId(schoolyear.id!, true);
        var teacherList = await daoFactory.teacherDao!.getAll();

        var latexBuilder = new OfficialEnrollmentListLatexBuilder.Builder(resource, $"{resource}/out/enrollments")
            .withDegreeList(degreeList)
            .withTeacherList(teacherList)
            .withUserName(userAlias)
            .build();

        setLatexBuilder(latexBuilder);
        return getPDF();
    }

    public async Task<byte[]> getDebtorReport(string userAlias)
    {
        var studentList = await daoFactory.accountingStudentDao!.getDebtorStudentList();
        var degreeList = await daoFactory.degreeDao!.getValidListForNewOrCurrentSchoolyear();

        var latexBuilder = new DebtorReportLatexBuilder.Builder(resource, $"{resource}/out/debtor")
            .withStudentList(studentList)
            .withDegreeList(degreeList)
            .withUserName(userAlias)
            .build();

        setLatexBuilder(latexBuilder);
        return getPDF();
    }

    public async Task<byte[]> getActiveCertificateByStudent(string studentId, string userAlias)
    {
        var student = await daoFactory.academyStudentDao!.getCurrentById(studentId);
        if (!student.student.isActive)
        {
            throw new ConflictException($"The student with id ({studentId}) is not active.");
        }

        var enrollment = await daoFactory.enrollmentDao!.getById(student.enrollmentId!);
        var degree = await daoFactory.degreeDao!.getById(enrollment!.degreeId);
        var schoolyear = await daoFactory.schoolyearDao!.getCurrent();

        var latexBuilder = new ActiveCertificateLatexBuilder.Builder(resource, $"{resource}/out/active")
            .withStudent(student)
            .withUserAlias(userAlias)
            .withEnrollment(enrollment.label)
            .withLevel(degree!.educationalLevel)
            .withSchoolyear(schoolyear.label)
            .build();

        setLatexBuilder(latexBuilder);
        return getPDF();
    }

    public async Task<byte[]> getProformaByStudent(string studentId, string userAlias)
    {
        var student = await daoFactory.academyStudentDao!.getCurrentById(studentId);
        var enrollment = await daoFactory.enrollmentDao!.getById(student.enrollmentId!);

        return await getProformaByDegree(enrollment!.degreeId, student.fullName(), userAlias);
    }

    public async Task<byte[]> getProformaByDegree(string degreeId, string name, string userAlias)
    {
        var degree = await daoFactory.degreeDao!.getById(degreeId);
        var schoolyear = await daoFactory.schoolyearDao!.getCurrent();

        var tariffList = await daoFactory.tariffDao!.getAll();
        tariffList = tariffList
            .Where(e => DegreeDataEntity.getLevelName(e.educationalLevel) == degree!.educationalLevel)
            .OrderBy(e => e.dueDate).ToList();

        var latexBuilder = new ProformaLatexBuilder.Builder(resource, $"{resource}/out/proforma")
            .withStudent(name)
            .withUserAlias(userAlias)
            .withDegree(degree!)
            .withSchoolyear(schoolyear.label)
            .withTariffList(tariffList)
            .build();

        setLatexBuilder(latexBuilder);
        return getPDF();
    }

    public async Task<byte[]> getAccountStatement(string studentId, string userAlias)
    {
        var student = await daoFactory.accountingStudentDao!.getFullById(studentId);
        var schoolyear = await daoFactory.schoolyearDao!.getCurrent();

        var latexBuilder = new AccountStatementLatexBuilder.Builder(resource, $"{resource}/out/statement")
            .withStudent(student)
            .withUserAlias(userAlias)
            .withSchoolyear(schoolyear)
            .withSchoolyearList(await daoFactory.schoolyearDao!.getAll())
            .build();

        setLatexBuilder(latexBuilder);
        return getPDF();
    }

    public async Task<byte[]> getAcademicRecord(string studentId, string schoolyearId, string userAlias)
    {
        var student = await daoFactory.studentDao!.getFullById(studentId);

        var schoolyear = await daoFactory.schoolyearDao!.getById(schoolyearId);
        if (schoolyear == null)
        {
            throw new EntityNotFoundException("SchoolyearEntity", schoolyearId);
        }

        var academic = await daoFactory.academyStudentDao!.getById(studentId, schoolyearId);
        if (academic == null)
        {
            throw new EntityNotFoundException($"The student ({studentId}) has no records for the schoolyear ({schoolyearId}).");
        }
        
        var partialList = await daoFactory.partialDao!.getListByEnrollmentId(academic.enrollmentId!);
        var enrollment = await daoFactory.enrollmentDao!.getById(academic.enrollmentId!);
        
        var latexBuilder = new AcademicRecordLatexBuilder.Builder(resource, $"{resource}/out/academic-record")
            .withStudent(student)
            .withUserAlias(userAlias)
            .withSchoolyear(schoolyear)
            .withPartialList(partialList)
            .withSubjectAreaList(await daoFactory.subjectAreaDao!.getAll())
            .withSubjectList(await daoFactory.academySubjectDao!.getByEnrollmentId(academic.enrollmentId!))
            .withEnrollmentLabel(enrollment!.label)
            .build();

        setLatexBuilder(latexBuilder);
        return getPDF();
    }
}