using wsmcbl.src.model.academy;

namespace wsmcbl.src.controller.business;

public interface IPrintReportCardByStudentController
{
    public Task<StudentEntity> getStudentInformation(string studentId);
    public Task<byte[]> getReportCard(string studentId, string partials);
}