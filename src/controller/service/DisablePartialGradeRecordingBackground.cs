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
        var partialList = await daoFactory.partialDao!.getListInCurrentSchoolyear();

        var item = partialList.FirstOrDefault(e => e.gradeRecordIsActive);
        if (item == null)
        {
            return;
        }
        
        var remainingTime = (TimeSpan)(item.gradeRecordDeadline - DateTime.UtcNow)!;
        
        if (remainingTime.TotalHours is <= 24 and > 22)
        {
            await sendNotification(item, remainingTime.TotalHours);
        }
        
        if (remainingTime.TotalHours < 0)
        {
            item.disableGradeRecording();
            await daoFactory.ExecuteAsync();
            await sendNotification(item);
        }
    }

    private async Task sendNotification(PartialEntity partial)
    {
        var date = partial.gradeRecordDeadline!.toStringUtc6();
        var message =
            $"Estimado docente, ha finalizado el registro de calificaciones para el {partial.label.ToUpper()} el {date}.\n" +
            "Ya no es posible modificar calificaciones en wsm.cbl-edu.com.";

        var list = await getEmailList();
        await emailNotifier.sendEmail(list,"Cierre del registro de calificaciones", message);
    }

    private async Task sendNotification(PartialEntity partial, double totalHours)
    {
        var date = partial.gradeRecordDeadline!.toStringUtc6();
        var message = "Estimado docente, " +
        $"el registro de calificaciones para el {partial.label.ToUpper()} cerrará dentro de {totalHours} horas,  el {date}.\n" +
        "Aún puede modificar calificaciones en wsm.cbl-edu.com.";
     
        var list = await getEmailList();   
        await emailNotifier.sendEmail(list,"Registro de calificaciones", message);
    }

    private async Task<List<string>> getEmailList()
    {
        var userList = await daoFactory.userDao!.getAll();
        
        return userList.Where(e => e.isActive && e.roleId != 1 && e.roleId != 3)
            .Select(e => e.email).ToList();
    }
}