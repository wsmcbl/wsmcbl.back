using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;
using wsmcbl.src.utilities;

namespace wsmcbl.src.controller.service;

public class DisablePartialGradeRecordingBackground : BackgroundService
{
    private readonly DaoFactory daoFactory;

    public DisablePartialGradeRecordingBackground(DaoFactory daoFactory)
    {
        this.daoFactory = daoFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await checkPartials();
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
            }

            await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
        }
    }

    private async Task checkPartials()
    {
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
        var userList = await daoFactory.userDao!.getAll();
        var list = userList.Where(e => e.isActive && e.roleId != 1 && e.roleId != 3)
            .Select(e => e.email).ToList();

        var date = partial.gradeRecordDeadline!.toStringUtc6();
        var message =
            $"Estimado docente, ha finalizado el registro de calificaciones para el {partial.label.ToUpper()} el día {date}.\n" +
            "Ya no es posible modificar calificaciones en wsm.cbl-edu.com.";
        
        await emailNotifier.sendEmail(list,"Cierre del registro de calificaciones", message);
    }
}