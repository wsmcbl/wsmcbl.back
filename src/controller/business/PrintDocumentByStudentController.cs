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

    private async Task<string> getUserAlias(string userId) => (await daoFactory.userDao!.getById(userId)).getAlias();
    
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

    public async Task<byte[]> getProformaDocument(string studentId, string userId)
    {
        return await documentMaker.getProformaByStudent(studentId, userId);
    }

    public async Task<byte[]> getProformaDocument(string? degreeId, string? name, string userId)
    {
        if (degreeId == null || name == null)
        {
            throw new InvalidDataException("degreeId and name must be provided.");
        }
        
        return await documentMaker.getProformaByDegree(degreeId, name, userId);
    }

    public async Task<byte[]> getAccountStatementDocument(string studentId, string userId)
    {
        return await documentMaker.getAccountStatement(studentId, await getUserAlias(userId));
    }
}