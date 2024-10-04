using wsmcbl.src.exception;
using wsmcbl.src.model;
using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;
using StudentEntity = wsmcbl.src.model.secretary.StudentEntity;

namespace wsmcbl.src.controller.business;

public class CreateStudentProfileController(DaoFactory daoFactory) : BaseController(daoFactory), ICreateStudentProfileController
{
    public async Task<StudentEntity> createStudent(StudentEntity student, StudentTutorEntity tutor, int modality)
    {
        var existingStudent = await daoFactory.studentDao!.getByInformation(student);
        if (existingStudent != null)
        {
            throw new ConflictException($"The student profile already exist with id ({existingStudent.studentId}).");
        }

        daoFactory.studentDao!.create(student);
        await daoFactory.execute();
        
        var existingTutor = await daoFactory.studentTutorDao!.getByInformation(tutor);
        if (existingTutor == null)
        {// Un tutor puede tener uno o más hijos
            tutor.studentId = student.studentId!;
            daoFactory.studentTutorDao!.create(tutor);
        }

        await createAccountingStudent(student.studentId!);
        // Assign registration debt refer to modality
        
        return student;
    }

    private async Task createAccountingStudent(string studentId)
    {
        var accountingStudent = new model.accounting.StudentEntity(studentId, Const.NON_DISCOUNT_ID);
        daoFactory.accountingStudentDao!.create(accountingStudent);
        await daoFactory.execute();
    }
}