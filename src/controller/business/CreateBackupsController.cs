using wsmcbl.src.exception;

namespace wsmcbl.src.controller.business;

public static class CreateBackupsController
{
    private const string BACKUP_DIR = "/backups";

    public static async Task<(byte[] data, string name)> getCurrentBackupDocument()
    {
        if (!Directory.Exists(BACKUP_DIR))
        {
            throw new NotFoundException("The backup directory does not exist.");
        }

        var latestBackup = new DirectoryInfo(BACKUP_DIR)
            .GetFiles("backup_*.sql")
            .OrderByDescending(f => f.CreationTime)
            .FirstOrDefault();

        if (latestBackup == null)
        {
            throw new NotFoundException("No backups available.");
        }

        var fileBytes = await File.ReadAllBytesAsync(latestBackup.FullName);
        return (fileBytes, latestBackup.Name);
    }
}