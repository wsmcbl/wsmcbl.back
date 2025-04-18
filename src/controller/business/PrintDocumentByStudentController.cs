using wsmcbl.src.controller.service.document;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class PrintDocumentByStudentController : BaseController
{
    private DocumentMaker documentMaker { get; set; }
    public PrintDocumentByStudentController(DaoFactory daoFactory) : base(daoFactory)
    {
        documentMaker = new DocumentMaker(daoFactory);   
    }
    
    public async Task<byte[]> getReportCard(string studentId, string userId)
    {
        return await documentMaker.getReportCardByStudent(studentId, userId);
    }

    public async Task<bool> isStudentSolvent(string studentId)
    {
        var debtHistoryList = await daoFactory.debtHistoryDao!.getListByStudentId(studentId);
        
        var debt = debtHistoryList.FirstOrDefault(e => e.isCurrentTariffMonthly());
        return debt != null && debt.isPaid;
    }

    public async Task<byte[]> getActiveCertificateDocument(string studentId, string userId)
    {
        return await documentMaker.getActiveCertificateByStudent(studentId, userId);
    }
}