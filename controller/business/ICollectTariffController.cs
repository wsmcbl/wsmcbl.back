using wsmcbl.back.model.accounting;

namespace wsmcbl.back.controller.business;

public interface ICollectTariffController
{
    public Task<StudentEntity?> getStudent(string id);
    public Task<List<StudentEntity>> getStudentsList();
    public void setStudentId(string studentId);
}