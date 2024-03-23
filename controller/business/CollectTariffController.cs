using wsmcbl.back.model.accounting;

namespace wsmcbl.back.controller.business;

public class CollectTariffController : ICollectTariffController
{
    private IStudentDao dao;
    
    public CollectTariffController(IStudentDao dao)
    {
        this.dao = dao;
    }
    
    public Task<StudentEntity?> getStudent(string id)
    {
        return dao.getById(id);
    }

    public Task<List<StudentEntity>> getStudentsList()
    {
        return dao.getAll();
    }

    public void setStudentId(string studentId)
    {
        
    }
}