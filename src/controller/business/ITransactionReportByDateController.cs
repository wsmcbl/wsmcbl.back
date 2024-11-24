using wsmcbl.src.model.accounting;

namespace wsmcbl.src.controller.business;

public interface ITransactionReportByDateController
{
    public Task<List<TransactionEntity>> getReportByDate();
}