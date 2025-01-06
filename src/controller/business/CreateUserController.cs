using wsmcbl.src.controller.service;
using wsmcbl.src.exception;
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
        var isDuplicate = await daoFactory.userDao!.isEmailDuplicate(user.email);
        if (isDuplicate)
        {
            throw new ConflictException($"A user with email ({user.email}) already exists.");
        }

        user.generateEmail();
        var password = generatePassword();
        userAuthenticator.encodePassword(user, password);
        
        daoFactory.userDao!.create(user);
        await daoFactory.execute();
        return user;
    }

    private string generatePassword()
    {
        return "Hola";
    }
}