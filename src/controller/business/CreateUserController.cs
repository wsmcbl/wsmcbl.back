using wsmcbl.src.controller.service;
using wsmcbl.src.model.config;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class CreateUserController : BaseController
{
    private UserAuthenticator userAuthenticator { get; }

    public CreateUserController(DaoFactory daoFactory, UserAuthenticator userAuthenticator) : base(daoFactory)
    {
        this.userAuthenticator = userAuthenticator; 
    }

    public async Task<List<UserEntity>> getUserList()
    {
        return await daoFactory.userDao!.getAll();
    }

    public async Task<UserEntity> createUser(UserEntity user)
    {
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

    private Task createNextcloudAccount(UserEntity user)
    {
        throw new NotImplementedException();
    }

    private Task createEmailAccount(UserEntity user)
    {
        throw new NotImplementedException();
    }

    private string generatePassword()
    {
        return "Hola";
    }

    public async Task addPermissions(List<int> permissionList, Guid userId)
    {
        await daoFactory.permissionDao!.checkListId(permissionList);
            
        var list = new List<UserPermissionEntity>();
        foreach (var item in permissionList)
        {
            list.Add(new UserPermissionEntity
            {
                userId = userId,
                permissionId = item
            });
        }
    }

    public async Task<List<PermissionEntity>> getPermissionList()
    {
        return await daoFactory.permissionDao!.getAll();
    }
}