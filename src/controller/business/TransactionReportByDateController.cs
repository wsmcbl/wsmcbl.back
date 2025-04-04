using System.Security.Authentication;
using wsmcbl.src.model;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class TransactionReportByDateController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    public async Task<PagedResult<TransactionReportView>> getPaginatedTransactionReportView(TransactionReportViewPagedRequest request)
    {
        return await daoFactory.transactionDao!.getPaginatedTransactionReportView(request);
    }
    
    public async Task<List<(int quantity, decimal total)>> getSummary(DateTime start, DateTime end)
    {
        var transactionList = await daoFactory.transactionDao!.getTransactionReportViewListByRange(start, end);
        
        (int quantity, decimal total) validSummary = (0, 0);
        (int quantity, decimal total) invalidSummary = (0, 0);
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
        var controller = new ApplyArrearsController(daoFactory);

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