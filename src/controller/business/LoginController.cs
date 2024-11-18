using wsmcbl.src.controller.service;
using wsmcbl.src.exception;
using wsmcbl.src.model.config;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class LoginController : BaseController, ILoginController
{
    private JwtGenerator jwtGenerator { get; }
    private UserAuthenticator userAuthenticator { get; }
    public LoginController(DaoFactory daoFactory, JwtGenerator jwtGenerator, UserAuthenticator userAuthenticator) : base(daoFactory)
    {
        this.jwtGenerator = jwtGenerator;
        this.userAuthenticator = userAuthenticator;
    }
    
    public async Task<string> getTokenByCredentials(UserEntity user)
    {
        var result = await userAuthenticator.authenticateUser(user);
        return result == null ? string.Empty : jwtGenerator.generateToken(result);
    }

    public async Task<UserEntity> createUser(UserEntity user)
    {
        var isDuplicate = await daoFactory.userDao!.isEmailDuplicate(user.email);
        if (isDuplicate)
        {
            throw new ConflictException($"A user with email ({user.email}) already exists.");
        }
        
        var password = user.password;
        userAuthenticator.EncodePassword(user, password);
        
        daoFactory.userDao!.create(user);
        await daoFactory.execute();

        user.password = "";
        return user;
    }

    public async Task<UserEntity> getUserById(string userId)
    {
        return await daoFactory.userDao!.getById(userId);
    }
}
