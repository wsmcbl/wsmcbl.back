using wsmcbl.src.exception;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class EnablePartialGradeRecordingController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    public async Task<List<PartialEntity>> getPartialList()
    {
        return await daoFactory.partialDao!.getAll();
    }

    public async Task enableGradeRecording(int partialId)
    {
        var partial = await daoFactory.partialDao!.getById(partialId);
        if (partial == null)
        {
            throw new EntityNotFoundException("PartialEntity", partialId.ToString());
        }

        partial.gradeRecordIsActive = true;
        await daoFactory.execute();
    }
}