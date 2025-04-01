using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class GeneratePerformanceReportBySectionController : BaseController
{
    public GeneratePerformanceReportBySectionController(DaoFactory daoFactory) : base(daoFactory)
    {
    }

    public async Task<object?> getEnrollmentPerformanceByTeacherId(string teacherId)
    {
        return await daoFactory.teacherDao!.getById(teacherId);
    }
}