using wsmcbl.src.model;
using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.business;

public class GenerateStudentRegisterController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    public async Task<PagedResult<StudentView>> getStudentRegisterList(StudentPagedRequest request)
    {
        return await daoFactory.studentDao!.getStudentViewList(request);
    }
}