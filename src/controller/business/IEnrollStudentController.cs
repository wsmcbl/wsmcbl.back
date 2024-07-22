using wsmcbl.src.model.academy;

namespace wsmcbl.src.controller.business;

public interface IEnrollStudentController
{
    public Task<List<StudentEntity>> getStudentList();
}