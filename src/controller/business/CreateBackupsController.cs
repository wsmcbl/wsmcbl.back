using wsmcbl.src.exception;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class CreateBackupsController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    private const string backupDirectory = "/backups";

    public (byte[] data, string name) getCurrentBackupDocument()
    {
        if (!Directory.Exists(backupDirectory))
        {
            throw new NotFoundException("The backup directory does not exist.");
        }

        var latestBackup = new DirectoryInfo(backupDirectory)
            .GetFiles("backup_*.sql")
            .OrderByDescending(f => f.CreationTime)
            .FirstOrDefault();

        if (latestBackup == null)
        {
            throw new NotFoundException("No backups available.");
        }

        var fileBytes = File.ReadAllBytes(latestBackup.FullName);
        return (fileBytes, latestBackup.Name);
    }
}