using wsmcbl.src.exception;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class CollectTariffController : BaseController, ICollectTariffController
{
    public CollectTariffController(DaoFactory daoFactory) : base(daoFactory)
    {
    }

    public async Task<List<StudentEntity>> getStudentsList()
    {
        return await daoFactory.studentDao!.getAll();
    }
    
    public async Task<StudentEntity> getStudent(string studentId)
    {
        var student = await daoFactory.studentDao!.getById(studentId);

        if (student is null)
        {
            throw new EntityNotFoundException("Student", studentId);
        }

        await student.loadDebtHistory(daoFactory.debtHistoryDao);
        
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
        
        daoFactory.tariffDao!.update(tariff);
        await daoFactory.execute();
    }
    
    public async Task<string> saveTransaction(TransactionEntity transaction, List<DebtHistoryEntity> debtList)
    {
        daoFactory.transactionDao!.create(transaction);
        await daoFactory.debtHistoryDao!.exonerateArrears(transaction.studentId, debtList);
        await daoFactory.execute();

        return transaction.transactionId!;
    }

    public async Task<(TransactionEntity, StudentEntity, CashierEntity, float[])> getFullTransaction(string transactionId)
    {
        var transaction = await daoFactory.transactionDao!.getById(transactionId);
        
        if (transaction is null)
        {
            throw new EntityNotFoundException("Transaction", transactionId);
        }
        
        var cashier = await daoFactory.cashierDao!.getById(transaction.cashierId);
        var student = await getStudent(transaction.studentId);
        
        var generalBalance = await daoFactory.tariffDao!.getGeneralBalance(transaction.studentId);

        return (transaction, student, cashier!, generalBalance);
    }

    public Task<List<TariffTypeEntity>> getTariffTypeList()
    {
        return daoFactory.tariffTypeDao!.getAll();
    }
}