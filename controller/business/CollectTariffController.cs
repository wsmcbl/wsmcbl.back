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
    

    public Task<List<StudentEntity>> getStudentsList()
    {
        return daoFactory.studentDao<StudentEntity>()!.getAll();
    }
    
    public Task<StudentEntity?> getStudent(string studentId)
    {
        var student = daoFactory.studentDao<StudentEntity>()!.getById(studentId);

        if (student is null)
        {
            throw new EntityNotFoundException("Student", studentId);
        }
        
        return student;
    }
    
    public Task<List<TariffEntity>> getTariffList()
    {
        return daoFactory.tariffDao!.getAll();
    }

    public Task<List<TariffEntity>> getTariffListByStudent(string studentId)
    {
        return daoFactory.tariffDao!.getListByStudent(studentId);
    }
    
    public Task<List<TariffEntity>> getOverdueTariffList()
    {
        return daoFactory.tariffDao!.getOverdueList();
    }

    public async Task applyArrears(int tariffId)
    {
        var tariff = await daoFactory.tariffDao!.getById(tariffId);

        if (tariff is null)
        {
            throw new EntityNotFoundException("Tariff", tariffId.ToString());
        }
        
        tariff.isLate = true;
        
        await daoFactory.tariffDao!.update(tariff);
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
    
    private Task<CashierEntity?> getCashier(string id)
    {
        return daoFactory.cashierDao!.getById(id);
    }
}