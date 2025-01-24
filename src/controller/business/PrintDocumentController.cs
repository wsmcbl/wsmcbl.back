using wsmcbl.src.controller.service;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class PrintDocumentController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    public async Task<byte[]> getOfficialEnrollmentListDocument(string userId)
    {
        var documentMaker = new DocumentMaker(daoFactory);
        return await documentMaker.getOfficialEnrollmentListDocument(userId);
    }
}