using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.business;

public interface IEnrollStudentController
{
    public Task<List<StudentEntity>> getStudentList();
    public Task<StudentEntity> getStudentById(string studentId);
    public Task<List<DegreeEntity>> getGradeList();
    public Task<StudentEntity> saveEnroll(StudentEntity student, string enrollmentId);
    public Task<byte[]> getEnrollDocument(string studentId);
}