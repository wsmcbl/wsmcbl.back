using wsmcbl.src.model.config;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class UpdateRolesController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    public async Task<List<RoleEntity>> getRoleList()
    {
        return await daoFactory.roleDao!.getAll();
    }
}