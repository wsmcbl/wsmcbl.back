using wsmcbl.src.controller.service.document;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class GenerateDebtorReportController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    public async Task<byte[]> getDebtorReport(string userId)
    {
        var documentMaker = new DocumentMaker(daoFactory);
        return await documentMaker.getDebtorReport(userId);
    }
}