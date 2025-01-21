using wsmcbl.src.controller.service;
using wsmcbl.src.model.config;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class LoginController : BaseController
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
        await result.getIdFromRole(daoFactory);
        return jwtGenerator.generateToken(result);
    }

    public async Task<UserEntity> getUserById(string userId)
    {
        return await daoFactory.userDao!.getById(userId);
    }
}
