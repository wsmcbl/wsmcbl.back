using wsmcbl.src.exception;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class MoveStudentFromEnrollmentController(DaoFactory daoFactory)
    : BaseController(daoFactory), IMoveStudentFromEnrollmentController
{
    public async Task<StudentEntity> changeStudentEnrollment(StudentEntity studentValue, string enrollmentId)
    {
        var enrollment = await getEnrollment(enrollmentId);
        
        var oldEnrollment = await daoFactory.enrollmentDao!.getById(studentValue.enrollmentId!);
        oldEnrollment!.quantity--;
        
        studentValue.enrollmentId = enrollmentId;
        enrollment.quantity++;

        daoFactory.academyStudentDao!.update(studentValue);
        daoFactory.enrollmentDao!.update(enrollment);
        await daoFactory.execute();

        return studentValue;
    }

    public async Task<StudentEntity> getValidStudent(string studentId)
    {
        var student = await daoFactory.academyStudentDao!.getById(studentId);

        if (student == null)
        {
            throw new EntityNotFoundException("student", studentId);
        }

        var current = await daoFactory.schoolyearDao!.getCurrentSchoolyear();
        if (student.schoolYear != current.id)
        {
            throw new ConflictException("This student cannot move from enrollment.");
        }

        return student;
    }

    private async Task<EnrollmentEntity> getEnrollment(string enrollmentId)
    {
        var enrollment = await daoFactory.enrollmentDao!.getById(enrollmentId);
        if (enrollment == null)
        {
            throw new EntityNotFoundException("enrollment", enrollmentId);
        }

        return enrollment;
    }
}