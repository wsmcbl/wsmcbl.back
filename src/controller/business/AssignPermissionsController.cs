using wsmcbl.src.exception;
using wsmcbl.src.model.config;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class AssignPermissionsController : BaseController
{
    private readonly CreateUserController userController;

    public AssignPermissionsController(DaoFactory daoFactory, CreateUserController userController) : base(daoFactory)
    {
        this.userController = userController;
    }

    public async Task<List<PermissionEntity>> getPermissionList()
    {
        return await daoFactory.permissionDao!.getAll();
    }

    public async Task<UserEntity> updateUser(UserEntity value, string nextCloudGroup)
    {
        var user = await daoFactory.userDao!.getById((Guid)value.userId!);
        if (user == null)
        {
            throw new EntityNotFoundException("UserEntity", value.userId!.ToString());
        }
        
        user.update(value);
        await daoFactory.execute();

        return user;
    }

    public async Task assignPermissions(List<int> permissionList, Guid userId)
    {
        await userController.addPermissions(permissionList, userId);
    }
}