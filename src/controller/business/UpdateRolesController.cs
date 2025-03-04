using wsmcbl.src.exception;
using wsmcbl.src.model.config;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class UpdateRolesController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    public async Task<List<RoleEntity>> getRoleList()
    {
        return await daoFactory.roleDao!.getAll();
    }

    public async Task<RoleEntity> getRoleById(int roleId)
    {
        var rol = await daoFactory.roleDao!.getById(roleId);
        if (rol == null)
        {
            throw new EntityNotFoundException("RoleEntity");
        }
        
        return rol;
    }

    public async Task<RoleEntity> updateRole(RoleEntity value)
    {
        var role = await getRoleById(value.roleId);
        
        role.setDescription(value.description);
        role.updateRolePermissionList(value.rolePermissionList, daoFactory.rolePermissionDao!);
        await daoFactory.execute();
            
        return role;
    }
}