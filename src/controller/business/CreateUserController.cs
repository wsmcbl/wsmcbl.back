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
        user.generateEmail(daoFactory.userDao!);
        
        var password = generatePassword();
        userAuthenticator.encodePassword(user, password);
        
        daoFactory.userDao!.create(user);
        await daoFactory.execute();
        
        user.password = password;
        return user;
    }

    private string generatePassword()
    {
        return "Hola";
    }

    public async Task addPermissions(List<int> permissionList, Guid userId)
    {
        var list = await daoFactory.permissionsDao.getByList(permissionList);

        var entities = new List<UserPermissionEntity>();
        foreach (var item in list)
        {
            entities.Add(new UserPermissionEntityy
            {
                userId = userId,
                permissionId = item.permissionId
            });
        }
    }
}