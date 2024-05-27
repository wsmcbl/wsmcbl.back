using wsmcbl.back.dto.output;
using wsmcbl.back.exception;
using wsmcbl.back.model.accounting;
using wsmcbl.back.model.dao;

namespace wsmcbl.back.controller.business;

public class CollectTariffController : BaseController, ICollectTariffController
{
    public CollectTariffController(DaoFactory daoFactory) : base(daoFactory)
    {
    }
    
    public Task<StudentEntity?> getStudent(string id)
    {
        var student = daoFactory.studentDao<StudentEntity>()!.getById(id);

        if (student is null)
        {
            throw new EntityNotFoundException("Student", id);
        }
        
        return student;
    }

    public Task<List<StudentEntity>> getStudentsList()
    {
        return daoFactory.studentDao<StudentEntity>()!.getAll();
    }

    public Task<List<TariffEntity>> getAllTariff()
    {
        return daoFactory.tariffDao!.getAll();
    }

    public async Task saveTransaction(TransactionEntity transaction)
    {
        await daoFactory.transactionDao!.create(transaction);
    }

    public async Task<InvoiceDto> getLastTransactionByStudent(string studentId)
    {
        var transaction = await daoFactory.transactionDao!.getLastByStudentId(studentId);
        
        if (transaction is null)
        {
            throw new EntityNotFoundException("Student", studentId);
        }
        
        var cashier = await getCashier(transaction.cashierId);
        var student = await getStudent(studentId);
        
        return transaction.mapToDto(student, cashier);
    }

    public Task<List<TariffEntity>> getUnexpiredTariff(string schoolyear)
    {
        return daoFactory.tariffDao!.getAll(schoolyear);
    }

    public async Task applyArrears(int tariffId)
    {
        var tariff = await daoFactory.tariffDao!.getById(tariffId);
        
        tariff!.isLate = true;
        
        await daoFactory.tariffDao!.update(tariff);
    }

    private Task<CashierEntity?> getCashier(string id)
    {
        return daoFactory.cashierDao!.getById(id);
    }
}