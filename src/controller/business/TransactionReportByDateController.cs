using System.Security.Authentication;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class TransactionReportByDateController(DaoFactory daoFactory) : BaseController(daoFactory),
    ITransactionReportByDateController
{
    public async Task<List<TransactionReportView>> getTransactionList(int range)
    {
        getDateRange(range);
        transactionList = await daoFactory.transactionDao!.getByRange(start, end);
        return transactionList;
    }

    public async Task<string> getUserName(string userId)
    {
        var result = await daoFactory.userDao!.getById(userId);

        if (result == null)
        {
            throw new AuthenticationException($"User with id ({userId}) not authenticate.");
        }

        return result.getAlias();
    }

    private DateTime start = DateTime.Now;
    private DateTime end = DateTime.Now;

    public (DateTime start, DateTime end) getDateRange(int range)
    {
        end = DateTime.Now;
        start = new DateTime(end.Year, end.Month, end.Day, 0, 0, 0);

        var today = DateTime.Today;
        switch (range)
        {
            case 2:
                start = new DateTime(today.Year, today.Month, today.Day, 0, 0, 0);
                end = new DateTime(today.Year, today.Month, today.Day, 23, 59, 59);
                break;
            case 3:
                start = new DateTime(today.Year, today.Month, 1, 0, 0, 0);
                end = new DateTime(today.Year, today.Month, end.Day, end.Hour, end.Minute, end.Second);
                break;
            default:
                start = new DateTime(today.Year, 1, 1, 0, 0, 0);
                end = new DateTime(today.Year, today.Month, end.Day, end.Hour, end.Minute, end.Second);
                break;
        }

        return (start, end);
    }

    private List<TransactionReportView> transactionList = [];
    public List<(int quantity, double total)> getSummary()
    {
        (int quantity, double total) validSummary = (0, 0);
        (int quantity, double total) invalidSummary = (0, 0);
        foreach (var item in transactionList)
        {
            if (item.isvalid)
            {
                validSummary.quantity++;
                validSummary.total += item.total;
            }
            else
            {
                invalidSummary.quantity++;
                invalidSummary.total += item.total;
            }
        }
        
        return [validSummary, invalidSummary];
    }
}