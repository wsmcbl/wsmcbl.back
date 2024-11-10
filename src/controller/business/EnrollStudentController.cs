using wsmcbl.src.controller.service;
using wsmcbl.src.exception;
using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.business;

public class EnrollStudentController(DaoFactory daoFactory) : BaseController(daoFactory), IEnrollStudentController
{
    public async Task<List<StudentEntity>> getStudentListWithSolvency()
    {
        return await daoFactory.studentDao!.getAllWithSolvency();
    }

    public async Task<StudentEntity> getStudentById(string studentId)
    {
        return await daoFactory.studentDao!.getByIdWithProperties(studentId);
    }

    public async Task<List<DegreeEntity>> getValidDegreeList()
    {
        return await daoFactory.degreeDao!.getValidListForTheSchoolyear();
    }

    public async Task<StudentEntity> saveEnroll(StudentEntity student, string enrollmentId)
    {
        var isStudentEnroll = await isAlreadyEnroll(student.studentId!);
        if (isStudentEnroll)
        {
            throw new ConflictException($"The student with id ({student.studentId}) is al ready enroll.");
        }
        
        await student.saveChanges(daoFactory);

        var academyStudent = await getNewAcademyStudent(student.studentId!, enrollmentId);
        daoFactory.academyStudentDao!.create(academyStudent);
        await daoFactory.execute();

        return student;
    }

    private async Task<bool> isAlreadyEnroll(string studentId)
    {
        var ids = await daoFactory.schoolyearDao!.getCurrentAndNewSchoolyearIds();
        var academyStudent = await daoFactory.academyStudentDao!.getLastById(studentId);

        return academyStudent != null && academyStudent.schoolYear == ids.newSchoolyear;
    }

    private async Task<model.academy.StudentEntity> getNewAcademyStudent(string studentId, string enrollmentId)
    {
        var schoolYear = await daoFactory.schoolyearDao!.getOrCreateNewSchoolyear();
        
        var academyStudent = new model.academy.StudentEntity(studentId, enrollmentId);
        
        academyStudent.setSchoolyear(schoolYear.id!);
        academyStudent.isNewEnroll();

        return academyStudent;
    }

    public async Task<byte[]> getEnrollDocument(string studentId)
    {
        var documentMaker = new DocumentMaker(daoFactory);
        return await documentMaker.getEnrollDocument(studentId);
    }

    public async Task<(string? enrollmentId, int discountId)> getEnrollmentAndDiscountByStudentId(string studentId)
    {
        var academyStudent = await daoFactory.academyStudentDao!.getLastById(studentId);
        var accountingStudent = await daoFactory.accountingStudentDao!.getWithoutPropertiesById(studentId);

        return (academyStudent?.enrollmentId, accountingStudent!.discountId);
    }

    public async Task updateStudentDiscount(string studentId, int discountId)
    {
        var accountingStudent = await daoFactory.accountingStudentDao!.getWithoutPropertiesById(studentId);
        accountingStudent.discountId = discountId;
        await daoFactory.execute();
    }
}