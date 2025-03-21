using wsmcbl.src.exception;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class EnablePartialGradeRecordingController : BaseController
{
    private DateTime now { get; }

    public EnablePartialGradeRecordingController(DaoFactory daoFactory) : base(daoFactory)
    {
        now = DateTime.UtcNow;
    }
    
    public async Task<List<PartialEntity>> getPartialList()
    {
        return await daoFactory.partialDao!.getListForCurrentSchoolyear();
    }

    public async Task enableGradeRecording(int partialId, DateTime deadline)
    {
        if (checkDateOrFail(deadline))
        {
            throw new BadRequestException("The deadline has to be greater than current date.");
        }

        var partial = await getPartialById(partialId);
        partial.enableGradeRecording(deadline);
        await daoFactory.ExecuteAsync();
    }

    private bool checkDateOrFail(DateTime deadline)
    {
        var nextHour = now.AddHours(1);
        var nextFortnight = now.AddDays(15);
        return deadline < nextHour || deadline > nextFortnight;
    }

    public async Task disableGradeRecording(int partialId)
    {
        var partial = await getPartialById(partialId);
        partial.disableGradeRecording();
        await daoFactory.ExecuteAsync();
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

    public async Task checkForPartialEnabledOrFail()
    {
        var list = await daoFactory.partialDao!.getListForCurrentSchoolyear();

        if (list.Where(e => e.gradeRecordIsActive).ToList().Count != 0)
        {
            throw new UpdateConflictException("Partial", "There is already a partial with the grade recording period active.");
        }
    }

    public async Task<PartialEntity> getPartialEnabled()
    {
        var list = await daoFactory.partialDao!.getListForCurrentSchoolyear();
        var result = list.Where(e => e.gradeRecordIsActive).ToList();
        if (result.Count == 0)
        {
            throw new EntityNotFoundException("Partial entity with gradeRecordIsActive enabled not found.");
        }
        
        return result.First();
    }

    public async Task activatePartial(int partialId, bool isActive)
    {
        var partial = await daoFactory.partialDao!.getById(partialId);
        if (partial == null)
        {
            throw new EntityNotFoundException("PartialEntity", partialId.ToString());
        }

        if(partial.isActive == isActive)
        {
            return;
        }

        if (!isActive && partial.gradeRecordIsActive)
        {
            throw new ConflictException("This partial cannot be deactivated because the grade record is active.");
        }
        
        partial.isActive = isActive;
        await daoFactory.ExecuteAsync();
    }
}