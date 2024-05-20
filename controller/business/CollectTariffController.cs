using wsmcbl.back.model.accounting;

namespace wsmcbl.back.controller.business;

public class CollectTariffController : ICollectTariffController
{
    private IStudentDao studentDao;
    private ITariffDao tariffDao;
    private ITransactionDao transactionDao;
    private ICashierDao cashierDao;
    
    public CollectTariffController(IStudentDao studentDao, ITariffDao tariffDao, ITransactionDao transactionDao, ICashierDao cashierDao)
    {
        this.studentDao = studentDao;
        this.tariffDao = tariffDao;
        this.transactionDao = transactionDao;
        this.cashierDao = cashierDao;
    }
    
    public Task<StudentEntity?> getStudent(string id)
    {
        return studentDao.getById(id);
    }

    public Task<List<StudentEntity>> getStudentsList()
    {
        return studentDao.getAll();
    }

    public Task<List<TariffEntity>> getTariffList()
    {
        return tariffDao.getAll();
    }

    public async Task saveTransaction(TransactionEntity transaction)
    {
        await transactionDao.create(transaction);
    }

    public async Task<TransactionEntity?> getLastTransactionByStudent(string studentId)
    {
        return await transactionDao.getLastByStudentId(studentId);
    }
    
    public Task<CashierEntity?> getCashier(string id)
    {
        return cashierDao.getById(id);
    }
}