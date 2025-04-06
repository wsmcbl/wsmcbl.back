using wsmcbl.src.controller.service.sheet;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class GeneratePerformanceReportBySectionController : BaseController
{
    public GeneratePerformanceReportBySectionController(DaoFactory daoFactory) : base(daoFactory)
    {
    }

    public async Task<List<StudentEntity>> getStudentListByTeacherId(string teacherId, int partial)
    { 
        var enrollmentId = await daoFactory.teacherDao!.getCurrentEnrollmentId(teacherId);
        return await daoFactory.academyStudentDao!.getListWithGradesForCurrentSchoolyear(enrollmentId, partial);
    }

    public async Task<byte[]> getEnrollmentGradeSummary(string teacherId, int partialId, string userId)
    {
        var enrollmentId = await daoFactory.teacherDao!.getCurrentEnrollmentId(teacherId);
        
        var sheetMaker = new SpreadSheetMaker(daoFactory);
        return await sheetMaker.getEnrollmentGradeSummary(enrollmentId, partialId, userId);
    }
}