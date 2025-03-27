using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class ViewDirectorDashboardController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    public async Task<int> getEnrolledStudent()
    {
        await Task.CompletedTask;
        return 0;
    }
}