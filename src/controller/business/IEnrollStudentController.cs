using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.business;

public interface IEnrollStudentController
{
    public Task<List<StudentEntity>> getStudentList();
    public Task<StudentEntity> getStudentById(string studentId);
    public Task<List<GradeEntity>> getGradeList();
    public Task<StudentEntity> saveEnroll(StudentEntity student, string enrollmentId);
    public Task getEnrollDocument(string studentId, Stream stream);
}