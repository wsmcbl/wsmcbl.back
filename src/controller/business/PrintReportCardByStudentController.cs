using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class PrintReportCardByStudentController(DaoFactory daoFactory)
    : BaseController(daoFactory), IPrintReportCardByStudentController
{
    public async Task<StudentEntity> getStudentScoreInformation(string studentId)
    {
        var result = await daoFactory.academyStudentDao!.getByIdInCurrentSchoolyear(studentId);
        var partials = await daoFactory.partialDao!.getListByCurrentSchoolyear();
        result.setPartials(partials);
        
        return result;
    }

    public async Task<byte[]> getReportCard(string studentId)
    {
        var printController = new PrintDocumentController(daoFactory);
        return await printController.getReportCardByStudent(studentId);
    }

    public async Task<TeacherEntity> getTeacherByEnrollment(string enrollmentId)
    {
        return await daoFactory.teacherDao!.getByEnrollmentId(enrollmentId);
    }
}