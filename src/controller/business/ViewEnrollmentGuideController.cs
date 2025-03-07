using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class ViewEnrollmentGuideController : BaseController
{
    public ViewEnrollmentGuideController(DaoFactory daoFactory) : base(daoFactory)
    {
    }
    
    public async Task<EnrollmentEntity> getEnrollmentGuideByTeacherId(string teacherId)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }
}