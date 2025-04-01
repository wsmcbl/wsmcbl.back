using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class GeneratePerformanceReportBySectionController : BaseController
{
    public GeneratePerformanceReportBySectionController(DaoFactory daoFactory) : base(daoFactory)
    {
    }

    public async Task<List<StudentEntity>> getEnrollmentPerformanceByTeacherId(string teacherId, int partial)
    { 
        var enrollment = await daoFactory.enrollmentDao!.getByTeacherIdForCurrentSchoolyear(teacherId);
        
        return await daoFactory
            .academyStudentDao!.getListWithGradesForCurrentSchoolyear(enrollment.enrollmentId, partial);
    }
}