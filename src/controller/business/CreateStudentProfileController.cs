using wsmcbl.src.exception;
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
        {
            daoFactory.studentTutorDao!.create(tutor);
        }
        
        return student;
    }
}