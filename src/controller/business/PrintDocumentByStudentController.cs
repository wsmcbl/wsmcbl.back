using wsmcbl.src.controller.service.document;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class PrintDocumentByStudentController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    public async Task<byte[]> getReportCard(string studentId, string userId)
    {
        var documentMaker = new DocumentMaker(daoFactory);
        return await documentMaker.getReportCardByStudent(studentId, userId);
    }

    public async Task<bool> isStudentSolvent(string studentId)
    {
        var debtHistoryList = await daoFactory.debtHistoryDao!.getListByStudentId(studentId);
        
        var debt = debtHistoryList.FirstOrDefault(e => e.isCurrentTariffMonthly());
        return debt != null && debt.isPaid;
    }
}