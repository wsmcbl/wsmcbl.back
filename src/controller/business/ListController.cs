using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.business;

public class ListController(DaoFactory daoFactory) : BaseController(daoFactory), IListController
{
    public async Task<List<StudentEntity>> getStudentList()
    {
        return await daoFactory.studentDao!.getAll();
    }

    public async Task<string> getMedia(int type, string schoolyear)
    {
        throw new NotImplementedException();
    }
}