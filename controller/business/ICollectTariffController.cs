using wsmcbl.back.model.entity.academy;

namespace wsmcbl.back.controller.business;

public interface ICollectTariffController
{
    public StudentEntity getStudent(string id);
    public List<StudentEntity> getStudentsList();
    public void setStudentId(string studentId);
}