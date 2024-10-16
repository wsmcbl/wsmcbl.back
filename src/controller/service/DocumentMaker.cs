using wsmcbl.src.exception;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.service;

public class DocumentMaker(DaoFactory daoFactory) : PdfMaker
{
    public async Task<byte[]> getReportCardByStudent(string studentId)
    {
        var student = await daoFactory.academyStudentDao!.getByIdInCurrentSchoolyear(studentId);
        var enrollment = await daoFactory.enrollmentDao!.getById(student.enrollmentId!);
        var teacher = await daoFactory.teacherDao!.getByEnrollmentId(student.enrollmentId!);

        if (teacher == null)
        {
            throw new EntityNotFoundException($"Teacher with enrollmentId ({student.enrollmentId}) not found.");
        }
            
        var partials = await daoFactory.partialDao!.getListByStudentId(studentId);
        student.setPartials(partials);
        
        var latexBuilder = new ReportCardLatexBuilder
            .Builder(resource, $"{resource}/out")
            .withStudent(student)
            .withTeacher(teacher)
            .withDegree(enrollment!.label)
            .withSubjects(await getSubjectSort(student.enrollmentId!))
            .withSemesters(await daoFactory.semesterDao!.getAllOfCurrentSchoolyear())
            .build();
        
        setLatexBuilder(latexBuilder);
        return getPDF();
    }

    private async Task<List<(string initials, string subjectId)>> getSubjectSort(string enrollmentId)
    {
        var subjectList = await daoFactory.subjectDao!.getByEnrollmentId(enrollmentId);
        
        return subjectList.Select(item => (item.getInitials, item.subjectId)).ToList();
    }
    
    public async Task<byte[]> getEnrollDocument(string studentId)
    {
        var student = await daoFactory.studentDao!.getByIdWithProperties(studentId);
        var enrollment = await daoFactory.enrollmentDao!.getByStudentId(student.studentId);
        
        var latexBuilder = new EnrollSheetLatexBuilder(resource, $"{resource}/out", student);
        latexBuilder.setGrade(enrollment.label);
        
        setLatexBuilder(latexBuilder);
        return getPDF();
    }

    public async Task<byte[]> getInvoiceDocument(string transactionId)
    {
        var transaction = await daoFactory.transactionDao!.getById(transactionId);
        if (transaction is null)
        {
            throw new EntityNotFoundException("Transaction", transactionId);
        }
        
        var cashier = await daoFactory.cashierDao!.getById(transaction.cashierId);
        if (cashier is null)
        {
            throw new EntityNotFoundException("Cashier", transaction.cashierId);
        }
        
        var student = await daoFactory.accountingStudentDao!.getById(transaction.studentId);
        if (student is null)
        {
            throw new EntityNotFoundException("Student", transaction.studentId);
        }
        
        var generalBalance = await daoFactory.tariffDao!.getGeneralBalance(transaction.studentId);
        
        var latexBuilder = new InvoiceLatexBuilder
            .Builder(resource, $"{resource}/out")
            .withStudent(student)
            .withTransaction(transaction)
            .withCashier(cashier)
            .withGeneralBalance(generalBalance)
            .withNumber(4517)
            .withSeries("A")
            .withExchangeRate(36.68f)
            .build();
        
        setLatexBuilder(latexBuilder);
        return getPDF();
    }
}