using wsmcbl.src.exception;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class MoveStudentFromEnrollmentController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    public async Task<StudentEntity> changeStudentEnrollment(StudentEntity studentValue, EnrollmentEntity enrollment)
    {
        /*
         * Se tiene que verificar si el primer semestre ya empezó para bloquear la acción de mover de grado (Solo a sección).
         */

        var oldEnrollment = await daoFactory.enrollmentDao!.getById(studentValue.enrollmentId!);
        oldEnrollment!.quantity--;

        enrollment.quantity++;
        daoFactory.enrollmentDao!.update(enrollment);
        await daoFactory.ExecuteAsync();

        await daoFactory.academyStudentDao!.update(studentValue.studentId, enrollment.enrollmentId!);
        studentValue.enrollmentId = enrollment.enrollmentId;

        return studentValue;
    }

    public async Task<StudentEntity> getStudentOrFailed(string studentId)
    {
        var student = await daoFactory.academyStudentDao!.getById(studentId);
        if (student == null)
        {
            throw new EntityNotFoundException("StudentEntity", studentId);
        }

        return student;
    }

    public async Task<EnrollmentEntity> getEnrollmentOrFailed(string enrollmentId, string oldEnrollmentId)
    {
        var enrollment = await daoFactory.enrollmentDao!.getById(enrollmentId);
        if (enrollment == null)
        {
            throw new EntityNotFoundException("EnrollmentEntity", enrollmentId);
        }

        var degree = await daoFactory.degreeDao!.getByEnrollmentId(enrollmentId);
        var oldDegree = await daoFactory.degreeDao!.getByEnrollmentId(oldEnrollmentId);
        if (degree!.educationalLevel != oldDegree!.educationalLevel)
        {
            throw new ConflictException("It is not possible to move this student to another educational level.");
        }

        return enrollment;
    }

    public async Task<bool> isThereAnActivePartial()
    {
        var partialList = await daoFactory.partialDao!.getListForCurrentSchoolyear();

        var result = partialList.FirstOrDefault(e => e is { isActive: true, gradeRecordIsActive: true });

        return result != null;
    }
}