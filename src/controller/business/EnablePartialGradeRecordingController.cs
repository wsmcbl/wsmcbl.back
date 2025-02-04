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

    public async Task enableGradeRecording(int partialId, DateTime deadline)
    {
        if (deadline < DateTime.Now)
        {
            throw new BadRequestException("The deadline has to be greater than current date.");
        }

        var partial = await getPartialById(partialId);
        partial.enableGradeRecording(deadline);
        await daoFactory.execute();
    }

    public async Task disableGradeRecording(int partialId)
    {
        var partial = await getPartialById(partialId);
        partial.disableGradeRecording();
        await daoFactory.execute();
    }

    private async Task<PartialEntity> getPartialById(int partialId)
    {
        var partial = await daoFactory.partialDao!.getById(partialId);
        if (partial == null)
        {
            throw new EntityNotFoundException("PartialEntity", partialId.ToString());
        }

        if (!partial.isActive)
        {
            throw new ConflictException($"The partial with id ({partialId}) is not active.");
        }

        return partial;
    }
}