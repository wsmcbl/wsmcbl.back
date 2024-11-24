using wsmcbl.src.model.accounting;

namespace wsmcbl.src.controller.business;

public interface ITransactionReportByDateController
{
    public Task<List<(TransactionEntity, StudentEntity)>> getTransactionList(int range);
    public Task<string> getUserName(string getAuthenticatedUserId);
    public Task<(DateTime start, DateTime end)> getDateRange(int range);
    public Task<List<(int quantity, double total)>> getSummary();
}