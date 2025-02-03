using wsmcbl.src.controller.service;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.accounting;
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

    public async Task<UserEntity> createUser(UserEntity user, string groupName)
    {
        await daoFactory.userDao!.isUserDuplicate(user);
        await user.generateEmail(daoFactory.userDao!);

        var password = generatePassword();
        userAuthenticator.encodePassword(user, password);

        daoFactory.userDao!.create(user);
        await daoFactory.execute();
        daoFactory.Detached(user);

        user.password = password;

        await createRole(user);
        await createEmailAccount(user);
        await createNextcloudAccount(user, groupName);
        
        return user;
    }

    private async Task createRole(UserEntity user)
    {
        if (user.roleId == 3)
        {
            var entity = new CashierEntity{ userId = (Guid)user.userId! };
            daoFactory.cashierDao!.create(entity);
            await daoFactory.execute();
        }
        
        if (user.roleId == 4)
        {
            var entity = new TeacherEntity((Guid)user.userId!);
            daoFactory.teacherDao!.create(entity);
            await daoFactory.execute();
        }
    }

    private async Task createNextcloudAccount(UserEntity user, string groupName)
    {
        var nextcloudUserCreator = new NextcloudUserCreator(httpClient);
        await nextcloudUserCreator.createUser(user);
        await nextcloudUserCreator.assignGroup(user.email, groupName.Trim());
    }

    private async Task createEmailAccount(UserEntity user)
    {
        var posteUserCreator = new PosteUserCreator(httpClient);
        await posteUserCreator.createUser(user);
    }

    private static string generatePassword()
    {
        var passwordGenerator = new PasswordGenerator();
        return passwordGenerator.generatePassword(10);
    }

    public async Task<List<string>> getNextcloudGroupList()
    {
        var nextcloudUserCreator = new NextcloudUserCreator(httpClient);
        return await nextcloudUserCreator.getGroupList();
    }
}