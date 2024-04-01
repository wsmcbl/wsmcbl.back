using wsmcbl.back.model.accounting;

namespace wsmcbl.back.controller.business;

public class CollectTariffController : ICollectTariffController
{
    private IStudentDao dao;
    private ITransactionDao tDao;
    
    public CollectTariffController(IStudentDao dao, ITransactionDao tDao)
    {
        this.dao = dao;
        this.tDao = tDao;
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
    
    
    public Task<TransactionEntity?> getTransaction(string id)
    {
        return tDao.getById(id);
    }
}