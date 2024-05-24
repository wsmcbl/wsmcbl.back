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

    public Task<List<TariffEntity>> getAllTariff()
    {
        return tariffDao.getAll();
    }

    public async Task saveTransaction(TransactionEntity transaction)
    {
        await transactionDao.create(transaction);
    }

    public async Task<InvoiceDto> getLastTransactionByStudent(string studentId)
    {
        var transaction = await transactionDao.getLastByStudentId(studentId);
        var cashier = await getCashier(transaction!.cashierId);
        var student = await getStudent(studentId);
        
        return transaction.mapToDto(student, cashier);
    }

    public Task<List<TariffEntity>> getUnexpiredTariff(string schoolyear)
    {
        return tariffDao.getAll(schoolyear);
    }

    public async Task applyArrears(int tariffId)
    {
        var tariff = await tariffDao.getById(tariffId);
        tariff!.isLate = true;
        
        tariffDao.update(tariff);
    }

    private Task<CashierEntity?> getCashier(string id)
    {
        return cashierDao.getById(id);
    }
}