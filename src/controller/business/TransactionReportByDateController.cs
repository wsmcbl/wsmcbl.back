using System.Security.Authentication;
using wsmcbl.src.model;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class TransactionReportByDateController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    public async Task<PagedResult<TransactionReportView>> getTransactionList(TransactionReportViewPagedRequest request)
    {
        return await daoFactory.transactionDao!.getAll(request);
    }
    
    public async Task<List<(int quantity, double total)>> getSummary(DateTime start, DateTime end)
    {
        var transactionList = await daoFactory.transactionDao!.getByRange(start, end);
        
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
    
    public async Task<string> getUserName(string userId)
    {
        var result = await daoFactory.userDao!.getById(userId);

        if (result == null)
        {
            throw new AuthenticationException($"User with id ({userId}) not authenticate.");
        }

        return result.getAlias();
    }
}