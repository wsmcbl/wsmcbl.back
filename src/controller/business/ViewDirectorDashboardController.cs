using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class ViewDirectorDashboardController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    public async Task<object?> getSummaryStudentQuantity()
    {
        await Task.CompletedTask;
        return 0;
    }

    public async Task<object?> getSummaryTeacherGrades()
    {
        await Task.CompletedTask;
        return 0;
    }
}