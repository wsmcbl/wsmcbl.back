using wsmcbl.src.exception;
using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;
using StudentEntity = wsmcbl.src.model.secretary.StudentEntity;

namespace wsmcbl.src.controller.business;

public class CreateStudentProfileController(DaoFactory daoFactory) : BaseController(daoFactory), ICreateStudentProfileController
{
    public async Task<StudentEntity> createStudent(StudentEntity student, StudentTutorEntity tutor)
    {
        var existingStudent = await daoFactory.studentDao!.getByInformation(student);
        if (existingStudent != null)
        {
            throw new ConflictException($"The student profile already exist with id ({existingStudent.studentId}).");
        }
        
        var existingTutor = await daoFactory.studentTutorDao!.getByInformation(tutor);
        if (existingTutor == null)
        {
            daoFactory.studentTutorDao!.create(tutor);
            await daoFactory.execute();
            existingTutor = tutor;
        }

        student.tutorId = existingTutor.tutorId!;
        daoFactory.studentDao!.create(student);
        await daoFactory.execute();
        
        return student;
    }

    public async Task createAccountingStudent(StudentEntity student, int educationalLevel)
    {
        var accountingStudent = new model.accounting.StudentEntity
        {
            studentId = student.studentId,
            discountId = 1,
            educationalLevel = educationalLevel,
            enrollmentLabel = null
        };
        
        daoFactory.accountingStudentDao!.create(accountingStudent);
        await daoFactory.execute();
    }
}