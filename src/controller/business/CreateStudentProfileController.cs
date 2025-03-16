using wsmcbl.src.exception;
using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.business;

public class CreateStudentProfileController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    public async Task<StudentEntity> createStudent(StudentEntity student, StudentTutorEntity tutor)
    {
        await checkForSchoolyearOrFail();
        
        var existingStudent = await daoFactory.studentDao!.findDuplicateOrNull(student);
        if (existingStudent != null)
        {
            throw new ConflictException($"The student profile already exist with id ({existingStudent.studentId}).");
        }
        
        var existingTutor = await daoFactory.studentTutorDao!.getByInformation(tutor);
        if (existingTutor == null)
        {
            daoFactory.studentTutorDao!.create(tutor);
            await daoFactory.ExecuteAsync();
            existingTutor = tutor;
        }

        student.tutorId = existingTutor.tutorId!;
        student.tutor = existingTutor;

        student.studentId = string.Empty;
        daoFactory.studentDao!.create(student);
        await daoFactory.ExecuteAsync();
        daoFactory.Detached(student);
        
        var value = await daoFactory.studentDao!.findDuplicateOrNull(student);
        if (value == null)
        {
            throw new InternalException();
        }
        
        student.studentId = value.studentId!;
        
        return student;
    }

    private async Task checkForSchoolyearOrFail()
    {
        try
        {
            await daoFactory.schoolyearDao!.getNewOrCurrent();
        }
        catch (Exception)
        {
            throw new EntityNotFoundException("There is no schoolyear.");
        }
    }

    public async Task createAccountingStudent(StudentEntity student, int educationalLevel)
    {
        var accountingStudent = new model.accounting.StudentEntity(student.studentId!, educationalLevel);
        daoFactory.accountingStudentDao!.create(accountingStudent);
        await daoFactory.ExecuteAsync();
    }
}