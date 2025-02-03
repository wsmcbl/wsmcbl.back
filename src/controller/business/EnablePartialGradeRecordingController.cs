using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class EnablePartialGradeRecordingController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    public async Task<List<PartialEntity>> getPartialList()
    {
        return await daoFactory.partialDao!.getAll();
    }
}