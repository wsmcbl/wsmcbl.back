using wsmcbl.src.exception;
using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.business;

public class UpdateStudentProfileController(DaoFactory daoFactory)
    : BaseController(daoFactory), IUpdateStudentProfileController
{
    public async Task updateStudent(StudentEntity student)
    {
        await student.saveChanges(daoFactory);
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
    
    public async Task updateStudentDiscount(string studentId, int discountId)
    {
        var accountingStudent = await daoFactory.accountingStudentDao!.getWithoutPropertiesById(studentId);

        accountingStudent.discountId = discountId switch
        {
            2 => accountingStudent.educationalLevel + 3,
            3 => accountingStudent.educationalLevel + 6,
            _ => accountingStudent.discountId
        };

        await daoFactory.execute();
    }
}