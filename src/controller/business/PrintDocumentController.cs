using wsmcbl.src.controller.service;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class PrintDocumentController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    public async Task<byte[]> getAssistanceListDocument()
    {
        var documentMaker = new DocumentMaker(daoFactory);
        return await documentMaker.getAssistanceListDocument();
    }
}