using wsmcbl.src.exception;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class CancelTransactionController(DaoFactory daoFactory) : BaseController(daoFactory), ICancelTransactionController
{
    public async Task<List<TransactionReportView>> getTransactionList()
    {
        return await daoFactory.transactionDao!.getViewAll();
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
        exitingTransaction.isValid = false;
        await daoFactory.execute();
        return exitingTransaction;
    }
}