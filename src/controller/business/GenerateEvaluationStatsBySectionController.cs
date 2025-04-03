using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class GenerateEvaluationStatsBySectionController : BaseController
{
    public GenerateEvaluationStatsBySectionController(DaoFactory daoFactory) : base(daoFactory)
    {
    }
    
    public async Task<List<StudentEntity>> getStudentListByTeacherId(string teacherId, int partial)
    {
        var controller = new GeneratePerformanceReportBySectionController(daoFactory);
        return await controller.getStudentListByTeacherId(teacherId, partial);
    }
}