using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.business;

public interface IEnrollStudentController
{
    public Task<List<StudentEntity>> getStudentListWithSolvency();
    public Task<StudentEntity> getStudentById(string studentId);
    public Task<List<DegreeEntity>> getValidDegreeList();
    public Task<StudentEntity> saveEnroll(StudentEntity student, string enrollmentId);
    public Task<byte[]> getEnrollDocument(string studentId);
    public Task<(string? enrollmentId, int discountId)> getEnrollmentAndDiscountByStudentId(string studentId);
}