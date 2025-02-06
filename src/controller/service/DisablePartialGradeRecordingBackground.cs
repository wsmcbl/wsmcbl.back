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

        try
        {
            await checkPartials(daoFactory);
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred: {e.Message}");
        }
    }

    private async Task checkPartials(DaoFactory daoFactory)
    {
        try
        {
            await sendNotification(new PartialEntity());
            return;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        
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
        var emailNotifier = new EmailNotifierService();
        await emailNotifier.sendEmail("admin@cbl-edu.com", "Subject", "Message");
    }
}