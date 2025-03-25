using wsmcbl.src.controller.service.document;
using wsmcbl.src.exception;
using wsmcbl.src.model;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class CancelTransactionController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    public async Task<PagedResult<TransactionReportView>> getPaginatedTransactionReportView(TransactionReportViewPagedRequest request)
    {
        return await daoFactory.transactionDao!.getPaginatedTransactionReportView(request);
    }

    public async Task<TransactionEntity> cancelTransaction(string transactionId)
    {
        var exitingTransaction = await daoFactory.transactionDao!.getById(transactionId);
        if (exitingTransaction == null)
        {
            throw new EntityNotFoundException("TransactionEntity", transactionId);
        }

        if (!exitingTransaction.isValid)
        {
            throw new UpdateConflictException("Transaction", "The transaction is already cancelled.");
        }

        await daoFactory.debtHistoryDao!.restoreDebt(transactionId);
        exitingTransaction.setAsInvalid();
        await daoFactory.ExecuteAsync();
        return exitingTransaction;
    }
    
    public async Task<byte[]> getInvoiceDocument(string transactionId)
    {
        var documentMaker = new DocumentMaker(daoFactory);
        return await documentMaker.getInvoiceDocument(transactionId);
    }
}