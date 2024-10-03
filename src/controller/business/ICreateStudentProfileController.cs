using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.business;

public interface ICreateStudentProfileController
{
    public Task<StudentEntity> createStudent(StudentEntity student, StudentTutorEntity tutor, int modality);
}