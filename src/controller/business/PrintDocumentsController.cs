using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class PrintDocumentsController(DaoFactory daoFactory) : BaseController(daoFactory), IPrintDocumentsController
{
    public async Task<byte[]> getEnrollDocument(string studentId)
    {
        throw new NotImplementedException();
    }
}