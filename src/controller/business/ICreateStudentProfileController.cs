using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.business;

public interface ICreateStudentProfileController
{
    public Task<StudentEntity> createStudent(StudentEntity student, StudentTutorEntity tutor);
    public Task createAccountingStudent(StudentEntity student, int educationalLevel);
}