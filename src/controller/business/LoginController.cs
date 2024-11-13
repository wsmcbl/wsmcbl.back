using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class LoginController(DaoFactory daoFactory) : BaseController(daoFactory), ILoginController
{
    
}
