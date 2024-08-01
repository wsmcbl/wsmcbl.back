using wsmcbl.src.exception;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class PrintReportCardByStudentController(DaoFactory daoFactory)
    : BaseController(daoFactory), IPrintReportCardByStudentController
{
    public async Task<StudentEntity> getStudentInformation(string studentId)
    {
        var result = await daoFactory.academyStudentDao!.getById(studentId);

        if (result == null)
        {
            throw new EntityNotFoundException("Academy Student", studentId);
        }
        
        return result;
    }

    public async Task<byte[]> getReportCard(string studentId, string partials)
    {
        var printController = new PrintDocumentController(daoFactory);
        return await printController.getReportCardByStudent(studentId);
    }
}