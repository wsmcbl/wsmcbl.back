using wsmcbl.src.exception;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class CreateBackupsController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    private const string backupDirectory = "/backups";

    public async Task<(byte[] data, string name)> getCurrentBackupDocument(string userId)
    {
        await checkUser(userId);
        
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

        var fileBytes = await File.ReadAllBytesAsync(latestBackup.FullName);
        return (fileBytes, latestBackup.Name);
    }
    
    private const int ADMIN_ROLE_ID = 1;
    private async Task checkUser(string userId)
    {
        try
        {
            var user = await daoFactory.userDao!.getById(userId);

            if (!user.isActive && user.role!.roleId != ADMIN_ROLE_ID)
            {
                throw new ArgumentException();
            }
        }
        catch (Exception)
        {
            throw new ForbiddenException("This user cannot perform this action.");
        }
    }
}