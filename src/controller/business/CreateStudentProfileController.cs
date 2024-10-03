using wsmcbl.src.exception;
using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.business;

public class CreateStudentProfileController(DaoFactory daoFactory)
    : BaseController(daoFactory), ICreateStudentProfileController
{
    public async Task<StudentEntity> createStudent(StudentEntity student, StudentTutorEntity tutor, int modality)
    {
        var existingTutor = await daoFactory.studentTutorDao!.getByInformation(tutor);
        var existingStudent = await daoFactory.studentDao!.getByInformation(student);
        
        if (existingStudent != null)
        {
            throw new ConflictException($"The student profile already exist with id ({existingStudent.studentId}).");
        }

        if (existingTutor == null)
        {
            daoFactory.studentTutorDao!.create(tutor);
        }

        daoFactory.studentDao!.create(student);
        await daoFactory.execute();

        // assign discount
        var accountingStudent = new model.accounting.StudentEntity();
        daoFactory.accountingStudentDao!.create(accountingStudent);
        await daoFactory.execute();
        
        // Assign registration debt refer to modality
        
        return student;
    }
}