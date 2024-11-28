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

    private DateTime start { get; set; }
    private DateTime end { get; set; }

    public (DateTime start, DateTime end) getDateRange(int range)
    {
        var now = DateTime.UtcNow;

        start = now.Date.AddHours(6);
        end = now;

        switch (range)
        {
            case 2:
                end = start.AddSeconds(-1);
                start = start.AddDays(-1);
                break;
            case 3:
                start = new DateTime(now.Year, now.Month, 1, 6, 0, 0, DateTimeKind.Utc);
                break;
            case 4:
                start = new DateTime(now.Year, 1, 1, 6, 0, 0, DateTimeKind.Utc);
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
            if (item.isValid)
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

    public async Task<List<TariffTypeEntity>> getTariffTypeList()
    {
        var controller = new CollectTariffController(daoFactory);

        var result = await controller.getTariffTypeList();
        result.Add(new TariffTypeEntity
        {
            typeId = 0,
            description = "Variados"
        });

        return result;
    }
}