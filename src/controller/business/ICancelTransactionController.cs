using wsmcbl.src.model.accounting;

namespace wsmcbl.src.controller.business;

public interface ICancelTransactionController
{
    public Task<List<TransactionReportView>> getTransactionList();
}