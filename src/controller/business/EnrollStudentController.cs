using wsmcbl.src.controller.service.document;
using wsmcbl.src.exception;
using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.business;

public class EnrollStudentController : BaseController
{
    private readonly UpdateStudentProfileController updateStudentProfileController;
    public EnrollStudentController(DaoFactory daoFactory) : base(daoFactory)
    {
        updateStudentProfileController = new UpdateStudentProfileController(daoFactory);
    }
    
    public async Task<List<StudentEntity>> getStudentListWithSolvency()
    {
        return await daoFactory.studentDao!.getAllWithRegistrationTariffPaid();
    }

    public async Task<StudentEntity> getStudentById(string studentId)
    {
        return await daoFactory.studentDao!.getByIdWithProperties(studentId);
    }

    public async Task<List<DegreeEntity>> getValidDegreeList()
    {
        var list = await daoFactory.degreeDao!.getValidListForTheSchoolyear();
        foreach (var item in list)
        {
            item.enrollmentList = item.enrollmentList!.Where(e => !e.isEnrollmentFull()).ToList();
        }

        return list;
    }

    public async Task<StudentEntity> saveEnroll(StudentEntity student, string enrollmentId, bool isRepeating)
    {
        var isStudentEnroll = await isAlreadyEnroll(student.studentId!);
        if (isStudentEnroll)
        {
            throw new ConflictException($"The student with id ({student.studentId}) is al ready enroll.");
        }
        
        await updateStudentProfileController.updateStudent(student, true);

        var academyStudent = await getNewAcademyStudent(student.studentId!, enrollmentId);
        academyStudent.setIsRepeating(isRepeating);
        daoFactory.academyStudentDao!.create(academyStudent);
        await daoFactory.execute();

        return student;
    }

    private async Task<bool> isAlreadyEnroll(string studentId)
    {
        var schoolyear = await daoFactory.schoolyearDao!.getNewOrCurrent();
        var academyStudent = await daoFactory.academyStudentDao!.getById(studentId);

        return academyStudent != null && academyStudent.schoolYear == schoolyear.id;
    }

    private async Task<model.academy.StudentEntity> getNewAcademyStudent(string studentId, string enrollmentId)
    {
        var schoolYear = await daoFactory.schoolyearDao!.getOrCreateNew();
        
        var academyStudent = new model.academy.StudentEntity(studentId, enrollmentId);
        
        academyStudent.setSchoolyear(schoolYear.id!);
        return academyStudent;
    }

    public async Task<byte[]> getEnrollDocument(string studentId, string userId)
    {
        var documentMaker = new DocumentMaker(daoFactory);
        return await documentMaker.getEnrollDocument(studentId, userId);
    }

    public async Task<(string? enrollmentId, int discountId, bool isRepeating)> getEnrollmentAndDiscountByStudentId(string studentId)
    {
        var academyStudent = await daoFactory.academyStudentDao!.getById(studentId);
        var accountingStudent = await daoFactory.accountingStudentDao!.getWithoutPropertiesById(studentId);

        var discountId = accountingStudent.discountId switch
        {
            < 3 => 1,
            > 3 and <= 6 => 2,
            > 6 and < 10 => 3,
            _ => accountingStudent.discountId
        };

        var isRepeating = academyStudent?.isRepeating ?? false;
        return (academyStudent?.enrollmentId, discountId, isRepeating);
    }

    public async Task updateStudentDiscount(string studentId, int discountId)
    {
        await updateStudentProfileController.updateStudentDiscount(studentId, discountId);
    }
}