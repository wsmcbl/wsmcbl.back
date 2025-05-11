using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;
using wsmcbl.src.utilities;

namespace wsmcbl.src.controller.service;

public class DisablePartialGradeRecordingBackground : BackgroundService
{
    private readonly DaoFactory daoFactory;
    private readonly EmailNotifierService emailNotifier;

    public DisablePartialGradeRecordingBackground(DaoFactory daoFactory)
    {
        this.daoFactory = daoFactory;
        emailNotifier = new EmailNotifierService();
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
        if (!Utility.isInProductionEnvironment()) return;

        var partialList = await daoFactory.partialDao!.getListForCurrentSchoolyear();

        var item = partialList.FirstOrDefault(e => e.recordIsActive());
        if (item == null)
        {
            return;
        }

        var now = DateTime.UtcNow;
        if (item.gradeRecordDeadline <= now)
        {
            await sendNotification(item);
            
            item.disableGradeRecording();
            daoFactory.partialDao.update(item);
            await daoFactory.ExecuteAsync();
            
            return;
        }

        await sendNotificationsByTime(item, (TimeSpan)(item.gradeRecordDeadline - now)!);
    }

    private async Task sendNotificationsByTime(PartialEntity item, TimeSpan timeSpan)
    {
        if (timeSpan is { Days: 7, Hours: 0})
        {
            await sendNotification(item, timeSpan);
        }
        
        if (timeSpan is { Days: 3, Hours: 0})
        {
            await sendNotification(item, timeSpan);
        }
        
        if (timeSpan.TotalHours is <= 24 and > 22)
        {
            await sendNotification(item, timeSpan);
        }
    }

    private async Task sendNotification(PartialEntity partial)
    {
        var date = partial.gradeRecordDeadline?.toString("dddd dd/MMMM/yyyy 'a las' h:mm tt");
        var message =
            $"Estimado docente, ha finalizado el registro de calificaciones para el {partial.label.ToUpper()} el {date}.\n" +
            "Ya no es posible modificar calificaciones en wsm.cbl-edu.com.";

        var list = await getEmailList();
        await emailNotifier.sendEmail(list, "Cierre del registro de calificaciones", message);
    }

    private async Task sendNotification(PartialEntity partial, TimeSpan time)
    {
        var date = partial.gradeRecordDeadline?.toString("dddd dd/MMMM/yyyy 'a las' h:mm tt");
        var message =
            $"Estimado docente, el registro de calificaciones para el {partial.label.ToUpper()} cerrará el {date}. " +
            $"Tiempo restante: {time.Days} día(s), {time.Hours} hora(s), {time.Minutes} minuto(s). " +
            "Aún puede modificar calificaciones en wsm.cbl-edu.com.";

        var list = await getEmailList();
        await emailNotifier.sendEmail(list, "Registro de calificaciones", message);
    }

    private async Task<List<string>> getEmailList()
    {
        var userList = await daoFactory.userDao!.getAll();
        return userList.Where(e => e.isActive && e.roleId != 1 && e.roleId != 3).Select(e => e.email).ToList();
    }
}