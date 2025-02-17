using wsmcbl.src.exception;
using wsmcbl.src.model;
using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.business;

public class UpdateStudentProfileController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    public async Task<StudentEntity> updateStudent(StudentEntity student, bool generateToken = false)
    {
        if (generateToken)
        {
            student.generateAccessToken();
        }
        
        await daoFactory.studentDao!.updateAsync(student);
        await daoFactory.studentTutorDao!.updateAsync(student.tutor);
        await daoFactory.studentFileDao!.updateAsync(student.file);
        await daoFactory.studentMeasurementsDao!.updateAsync(student.measurements);

        foreach (var parent in student.parents!)
        {
            await daoFactory.studentParentDao!.updateAsync(parent);
        }

        await daoFactory.execute();

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
        await daoFactory.execute();
    }

    public async Task updateStudentDiscount(string studentId, int discountId)
    {
        var accountingStudent = await daoFactory.accountingStudentDao!.getById(studentId);
        if (accountingStudent == null)
        {
            throw new EntityNotFoundException("StudentEntity", studentId);
        }
        
        accountingStudent.updateDiscountId(discountId);

        await daoFactory.execute();
    }

    public async Task<StudentEntity> getStudentById(string studentId)
    {
        return await daoFactory.studentDao!.getFullById(studentId);
    }

    public async Task<PagedResult<StudentView>> getStudentList(PagedRequest request)
    {
        return await daoFactory.studentDao!.getStudentViewList(request);
    }
}