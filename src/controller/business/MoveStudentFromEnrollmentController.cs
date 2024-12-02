using wsmcbl.src.exception;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class MoveStudentFromEnrollmentController(DaoFactory daoFactory)
    : BaseController(daoFactory), IMoveStudentFromEnrollmentController
{
    public async Task<StudentEntity> changeStudentEnrollment(string studentId, string enrollmentId)
    {
        var student = await daoFactory.academyStudentDao!.getById(studentId);

        if (student == null)
        {
            throw new EntityNotFoundException("student", studentId);
        }

        if (student.enrollmentId == enrollmentId)
        {
            throw new ConflictException("The student is already in this enrollment.");
        }

        var enrollment = await daoFactory.enrollmentDao!.getById(enrollmentId);
        if (enrollment == null)
        {
            throw new EntityNotFoundException("enrollment", enrollmentId);
        }
        
        var oldEnrollment = await daoFactory.enrollmentDao!.getById(student.enrollmentId!);
        oldEnrollment!.quantity--;
        
        student.enrollmentId = enrollmentId;
        enrollment.quantity++;

        await daoFactory.execute();

        return student;
    }
}