using wsmcbl.src.exception;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class MoveStudentFromEnrollmentController(DaoFactory daoFactory)
    : BaseController(daoFactory), IMoveStudentFromEnrollmentController
{
    public async Task<StudentEntity> changeStudentEnrollment(string studentId, string enrollmentId)
    {
        var student = await isStudentValid(studentId, enrollmentId);
        var enrollment = await getEntities(enrollmentId);
        
        var oldEnrollment = await daoFactory.enrollmentDao!.getById(student.enrollmentId!);
        oldEnrollment!.quantity--;
        
        student.enrollmentId = enrollmentId;
        enrollment.quantity++;

        daoFactory.academyStudentDao!.update(student);
        daoFactory.enrollmentDao!.update(enrollment);
        await daoFactory.execute();

        return student;
    }

    public async Task<StudentEntity> isStudentValid(string studentId, string enrollmentId)
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

        if (student.enrollmentId == enrollmentId)
        {
            throw new ConflictException("The student is already in this enrollment.");
        }

        return student;
    }

    private async Task<EnrollmentEntity> getEntities(string enrollmentId)
    {
        var enrollment = await daoFactory.enrollmentDao!.getById(enrollmentId);
        if (enrollment == null)
        {
            throw new EntityNotFoundException("enrollment", enrollmentId);
        }

        return enrollment;
    }
}