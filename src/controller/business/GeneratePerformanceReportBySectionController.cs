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
}