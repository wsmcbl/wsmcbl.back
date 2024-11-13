using wsmcbl.src.controller.service;
using wsmcbl.src.model.config;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class LoginController : BaseController, ILoginController
{
    private JwtGenerator jwtGenerator { get; }
    public LoginController(DaoFactory daoFactory, JwtGenerator jwtGenerator) : base(daoFactory)
    {
        this.jwtGenerator = jwtGenerator;
    }
    
    public async Task<string> getTokenByCredentials(UserEntity user)
    {
        var result = await daoFactory.userDao.authenticateUser(user.email);
        return result == null ? string.Empty : jwtGenerator.generateToken(result);
    }
}
