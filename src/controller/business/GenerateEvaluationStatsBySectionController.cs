using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class GenerateEvaluationStatsBySectionController : BaseController
{
    public GenerateEvaluationStatsBySectionController(DaoFactory daoFactory) : base(daoFactory)
    {
    }
    
    public async Task<object?> getEvaluationStatsByTeacherId(string teacherId, int partial)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }
}