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
    

    public async Task<List<StudentEntity>> getStudentsList()
    {
        return await daoFactory.studentDao<StudentEntity>()!.getAll();
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

        if (tariff.isLate)
        {
            throw new EntityUpdateException("The property isLate is already true.");
        }
        
        tariff.isLate = true;
        
        await daoFactory.tariffDao!.update(tariff);
    }
    
    public async Task<string> saveTransaction(TransactionEntity transaction)
    {
        await daoFactory.transactionDao!.create(transaction);

        return transaction.transactionId!;
    }

    public async Task<InvoiceDto> getFullTransaction(string transactionId)
    {
        var transaction = await daoFactory.transactionDao!.getById(transactionId);
        
        if (transaction is null)
        {
            throw new EntityNotFoundException("Transaction", transactionId);
        }
        
        var cashier = await getCashier(transaction.cashierId);
        var student = await getStudent(transaction.studentId);
        
        var dto = transaction.mapToDto(student, cashier);
        
        dto.generalBalance = await daoFactory.tariffDao!.getGeneralBalance(transaction.studentId);
        
        return dto;
    }

    public Task<List<TariffTypeEntity>> getTariffTypeList()
    {
        return daoFactory.tariffTypeDao!.getAll();
    }

    private Task<CashierEntity?> getCashier(string id)
    {
        return daoFactory.cashierDao!.getById(id);
    }
}