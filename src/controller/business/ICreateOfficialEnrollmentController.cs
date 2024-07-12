using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.business;

public interface ICreateOfficialEnrollmentController
{
    public Task<List<StudentEntity>> getStudentList();
    public Task saveStudent(StudentEntity student);
    public Task<List<GradeEntity>> getGradeList();
}