using wsmcbl.src.exception;
using wsmcbl.src.model;
using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.business;

public class UpdateStudentProfileController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    public async Task<StudentEntity> updateStudent(StudentEntity student)
    {
        await daoFactory.studentDao!.updateBy(student);
        await daoFactory.studentTutorDao!.updateAsync(student.tutor);
        await daoFactory.studentFileDao!.updateAsync(student.file);
        await daoFactory.studentMeasurementsDao!.updateAsync(student.measurements);

        foreach (var parent in student.parents!)
        {
            await daoFactory.studentParentDao!.updateAsync(parent);
        }

        await daoFactory.ExecuteAsync();

        return student;
    }

    public async Task updateProfilePicture(string studentId, byte[] picture)
    {
        var student = await daoFactory.studentDao!.getById(studentId);
        if (student == null)
        {
            throw new EntityNotFoundException("StudentEntity", studentId);
        }

        student.profilePicture = picture;
        await daoFactory.ExecuteAsync();
    }

    public async Task updateStudentDiscount(string studentId, int discountId)
    {
        var accountingStudent = await daoFactory.accountingStudentDao!.getById(studentId);
        if (accountingStudent == null)
        {
            throw new EntityNotFoundException("StudentEntity", studentId);
        }
        
        accountingStudent.updateDiscountId(discountId);

        await daoFactory.ExecuteAsync();
    }

    public async Task<StudentEntity> getStudentById(string studentId)
    {
        return await daoFactory.studentDao!.getFullById(studentId);
    }

    public async Task<PagedResult<StudentView>> getPaginatedStudentView(StudentPagedRequest request)
    {
        return await daoFactory.studentDao!.getPaginatedStudentView(request);
    }

    public async Task changeProfileState(string studentId)
    {
        var student = await daoFactory.studentDao!.getById(studentId);
        if (student == null)
        {
            throw new EntityNotFoundException("StudentEntity", studentId);
        }

        if (await isEnrolled(studentId) && student.isActive)
        {
            throw new ConflictException("The student is enrolled, cannot be disable.");
        }

        student.changeState();
        daoFactory.studentDao!.update(student);
        await daoFactory.ExecuteAsync();
    }

    private async Task<bool> isEnrolled(string studentId)
    {
        try
        {
            await daoFactory.academyStudentDao!.getCurrentById(studentId);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task changeProfileToken(string studentId)
    {
        var student = await daoFactory.studentDao!.getById(studentId);
        if (student == null)
        {
            throw new EntityNotFoundException("StudentEntity", studentId);           
        }
        
        student.generateAccessToken();
        daoFactory.studentDao!.update(student);
        await daoFactory.ExecuteAsync();
    }

    public async Task<(StudentView view, string? accessToken)> getProfileWithToken(string studentId)
    {
        var student = await daoFactory.studentDao!.getById(studentId);
        if (student == null)
        {
            throw new EntityNotFoundException("StudentEntity", studentId);           
        }

        var view = await daoFactory.studentDao!.getViewById(studentId);

        return (view, student.accessToken);
    }
}