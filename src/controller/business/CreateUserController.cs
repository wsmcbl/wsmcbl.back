using wsmcbl.src.controller.service;
using wsmcbl.src.model.config;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class CreateUserController : BaseController
{
    private HttpClient httpClient { get; }
    private UserAuthenticator userAuthenticator { get; }

    public CreateUserController(DaoFactory daoFactory, UserAuthenticator userAuthenticator, HttpClient httpClient) :
        base(daoFactory)
    {
        this.httpClient = httpClient;
        this.userAuthenticator = userAuthenticator;
    }

    public async Task<List<UserEntity>> getUserList()
    {
        return await daoFactory.userDao!.getAll();
    }

    public async Task<List<PermissionEntity>> getPermissionList()
    {
        return await daoFactory.permissionDao!.getAll();
    }

    public async Task<UserEntity> createUser(UserEntity user)
    {
        await daoFactory.userDao.isUserDuplicate(user);
        await user.generateEmail(daoFactory.userDao!);

        var password = generatePassword();
        userAuthenticator.encodePassword(user, password);

        daoFactory.userDao!.create(user);
        await daoFactory.execute();

        user.password = password;

        await createEmailAccount(user);
        await createNextcloudAccount(user);
        return user;
    }

    private async Task createNextcloudAccount(UserEntity user)
    {
        var nextcloudUserCreator = new NextcloudUserCreator();
        await nextcloudUserCreator.createUser(httpClient, user);
    }

    private async Task createEmailAccount(UserEntity user)
    {
        await Task.CompletedTask;
    }

    private static string generatePassword()
    {
        var passwordGenerator = new PasswordGenerator();
        return passwordGenerator.GeneratePassword(8);
    }

    public async Task addPermissions(List<int> permissionList, Guid userId)
    {
        await daoFactory.permissionDao!.checkListId(permissionList);

        foreach (var item in permissionList)
        {
            var userPermission = new UserPermissionEntity
            {
                userId = userId,
                permissionId = item
            };

            daoFactory.userPermissionDao!.create(userPermission);
        }

        await daoFactory.execute();
    }
}