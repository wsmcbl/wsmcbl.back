using wsmcbl.src.controller.service.document;
using wsmcbl.src.model;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class CollectTariffController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    public async Task<PagedResult<StudentView>> getStudentList(PagedRequest request)
    {
        return await daoFactory.accountingStudentDao!.getStudentViewList(request);
    }
    
    public async Task<StudentEntity> getStudentById(string studentId)
    {
        return await daoFactory.accountingStudentDao!.getFullById(studentId);
    }
    
    public Task<List<TariffEntity>> getTariffListByStudent(string studentId)
    {
        return daoFactory.tariffDao!.getListByStudent(studentId);
    }
    
    public async Task<TransactionEntity> saveTransaction(TransactionEntity transaction, List<DebtHistoryEntity> debtList)
    {
        if (await daoFactory.debtHistoryDao!.haveTariffsAlreadyPaid(transaction))
        {
            throw new ArgumentException("Some tariff is already paid.");
        }
        
        await daoFactory.debtHistoryDao!.exonerateArrears(transaction.studentId, debtList);
        daoFactory.transactionDao!.create(transaction);
        await daoFactory.execute();

        return transaction;
    }

    public async Task<byte[]> getInvoiceDocument(string transactionId)
    {
        var documentMaker = new DocumentMaker(daoFactory);
        return await documentMaker.getInvoiceDocument(transactionId);
    }
}