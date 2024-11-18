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

    public async Task<StudentEntity> saveEnroll(StudentEntity student, string enrollmentId, bool isRepeating)
    {
        var isStudentEnroll = await isAlreadyEnroll(student.studentId!);
        if (isStudentEnroll)
        {
            throw new ConflictException($"The student with id ({student.studentId}) is al ready enroll.");
        }

        student.accessToken = generateAccessToken();
        await student.saveChanges(daoFactory);

        var academyStudent = await getNewAcademyStudent(student.studentId!, enrollmentId);
        academyStudent.setIsRepeating(isRepeating);
        daoFactory.academyStudentDao!.create(academyStudent);
        await daoFactory.execute();

        return student;
    }

    private static string generateAccessToken()
    {
        var random = new Random();
        return random.Next(100000, 1000000).ToString();
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
        return academyStudent;
    }

    public async Task<byte[]> getEnrollDocument(string studentId, string userId)
    {
        var documentMaker = new DocumentMaker(daoFactory);
        return await documentMaker.getEnrollDocument(studentId, userId);
    }

    public async Task<(string? enrollmentId, int discountId, bool isRepeating)> getEnrollmentAndDiscountByStudentId(string studentId)
    {
        var academyStudent = await daoFactory.academyStudentDao!.getLastById(studentId);
        var accountingStudent = await daoFactory.accountingStudentDao!.getWithoutPropertiesById(studentId);

        var discountId = accountingStudent!.discountId switch
        {
            < 3 => 1,
            > 3 and <= 6 => 2,
            > 6 and < 10 => 3,
            _ => accountingStudent!.discountId
        };

        var isRepeating = academyStudent?.isRepeating ?? false;
        return (academyStudent?.enrollmentId, discountId, isRepeating);
    }

    public async Task updateStudentDiscount(string studentId, int discountId)
    {
        var accountingStudent = await daoFactory.accountingStudentDao!.getWithoutPropertiesById(studentId);
        accountingStudent.discountId = discountId != 2 ? discountId :
            discountId + accountingStudent.educationalLevel + 1;
        await daoFactory.execute();
    }

    public async Task updateProfilePicture(string studentId, byte[] picture)
    {
        var student = await daoFactory.studentDao!.getById(studentId);
        if (student == null)
        {
            throw new EntityNotFoundException("Student", studentId);
        }
        
        student.profilePicture = picture;
        await daoFactory.execute();
    }
}