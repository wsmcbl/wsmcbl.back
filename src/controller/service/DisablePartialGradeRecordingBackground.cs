using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.service;

public class DisablePartialGradeRecordingBackground : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public DisablePartialGradeRecordingBackground(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await checkPartials();
            await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
        }
    }

    private async Task checkPartials()
    {
        using var scope = _scopeFactory.CreateScope();
        var daoFactory = scope.ServiceProvider.GetRequiredService<DaoFactory>();

        var partialList = await daoFactory.partialDao!.getListInCurrentSchoolyear();
        
        var item = partialList.FirstOrDefault(e => e.gradeRecordIsActive);
        if (item == null)
        {
            return;
        }

        if (DateTime.UtcNow > item.gradeRecordDeadline)
        {
            item.disableGradeRecording();
            await daoFactory.execute();
            await sendNotification(item);
        }
    }

    private async Task sendNotification(PartialEntity partial)
    {
        await Task.CompletedTask;
    }
}