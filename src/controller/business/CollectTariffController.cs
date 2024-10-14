using wsmcbl.src.controller.service;
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
        return await daoFactory.accountingStudentDao!.getAll();
    }
    
    public async Task<StudentEntity> getStudentById(string studentId)
    {
        var student = await daoFactory.accountingStudentDao!.getById(studentId);

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

    public async Task<TariffEntity> applyArrears(int tariffId)
    {
        if (tariffId <= 0)
        {
            throw new BadRequestException("Invalid ID.");
        }
        
        var tariff = await daoFactory.tariffDao!.getById(tariffId);
        if (tariff is null)
        {
            throw new EntityNotFoundException("Tariff", tariffId.ToString());
        }
        
        tariff.isLate = true;
        
        daoFactory.tariffDao!.update(tariff);
        await daoFactory.execute();
        
        return tariff;
    }
    
    public async Task<TransactionEntity> saveTransaction(TransactionEntity transaction, List<DebtHistoryEntity> debtList)
    {
        if (await daoFactory.debtHistoryDao!.haveTariffsAlreadyPaid(transaction))
        {
            throw new ArgumentException($"Some tariff is already paid");
        }
        
        await daoFactory.debtHistoryDao!.exonerateArrears(transaction.studentId, debtList);
        daoFactory.transactionDao!.create(transaction);
        await daoFactory.execute();

        return transaction;
    }

    public Task<List<TariffTypeEntity>> getTariffTypeList()
    {
        return daoFactory.tariffTypeDao!.getAll();
    }

    public async Task<byte[]> getInvoiceDocument(string transactionId)
    {
        var documentMaker = new DocumentMaker(daoFactory);
        return await documentMaker.getInvoiceDocument(transactionId);
    }
}