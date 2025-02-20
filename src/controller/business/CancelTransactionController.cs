using wsmcbl.src.exception;
using wsmcbl.src.model;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class CancelTransactionController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    public async Task<PagedResult<TransactionReportView>> getTransactionList(TransactionReportViewPagedRequest request)
    {
        return await daoFactory.transactionDao!.getAll(request);
    }

    public async Task<TransactionEntity> cancelTransaction(string transactionId)
    {
        var exitingTransaction = await daoFactory.transactionDao!.getById(transactionId);
        if (exitingTransaction == null)
        {
            throw new EntityNotFoundException("transaction", transactionId);
        }

        if (!exitingTransaction.isValid)
        {
            throw new ConflictException("The transaction is already cancelled.");
        }

        await daoFactory.debtHistoryDao!.restoreDebt(transactionId);
        exitingTransaction.setAsInvalid();
        await daoFactory.execute();
        return exitingTransaction;
    }
}