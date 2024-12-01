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

        return student;
    }
}