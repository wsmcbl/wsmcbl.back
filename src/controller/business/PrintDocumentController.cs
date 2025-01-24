using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class PrintDocumentController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    public async Task<byte[]> getAssistanceListDocument()
    {
        throw new NotImplementedException();
    }
}