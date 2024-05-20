using wsmcbl.back.dto.output;
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

    public async Task<TransactionDtoService?> getLastTransactionByStudent(string studentId)
    {
        var transaction = await transactionDao.getLastByStudentId(studentId);
        var cashier = await getCashier(transaction!.cashierId);
        var student = await getStudent(studentId);

        return new TransactionDtoService(transaction, student!, cashier!);
    }
    
    private Task<CashierEntity?> getCashier(string id)
    {
        return cashierDao.getById(id);
    }
}