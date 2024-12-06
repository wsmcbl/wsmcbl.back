using wsmcbl.src.exception;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class MoveStudentFromEnrollmentController(DaoFactory daoFactory)
    : BaseController(daoFactory), IMoveStudentFromEnrollmentController
{
    public async Task<StudentEntity> changeStudentEnrollment(StudentEntity studentValue, EnrollmentEntity enrollment)
    {
        /*
         * Se tiene que verificar si el primer semestre ya empezó para bloquear la acción de mover de grado (Solo a sección).
         */
        
        var oldEnrollment = await daoFactory.enrollmentDao!.getById(studentValue.enrollmentId!);
        oldEnrollment!.quantity--;
        
        studentValue.enrollmentId = enrollment.enrollmentId;
        enrollment.quantity++;

        daoFactory.academyStudentDao!.update(studentValue);
        daoFactory.enrollmentDao!.update(enrollment);
        await daoFactory.execute();

        return studentValue;
    }

    public async Task<StudentEntity> getStudentOrFailed(string studentId)
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

    public async Task<EnrollmentEntity> getEnrollmentOrFailed(string enrollmentId, string oldEnrollmentId)
    {
        var enrollment = await daoFactory.enrollmentDao!.getById(enrollmentId);
        if (enrollment == null)
        {
            throw new EntityNotFoundException("enrollment", enrollmentId);
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
        var partialList = await daoFactory.partialDao!.getListByCurrentSchoolyear();

        var result = partialList.FirstOrDefault(e => e.gradeRecordIsActive);
        
        return result != null;
    }
}