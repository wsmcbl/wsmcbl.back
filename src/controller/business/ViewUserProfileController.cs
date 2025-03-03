using wsmcbl.src.model.config;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class ViewUserProfileController : BaseController
{
    private UpdateUserController updateUserController { get; }

    public ViewUserProfileController(DaoFactory daoFactory, UpdateUserController updateUserController) :
        base(daoFactory)
    {
        this.updateUserController = updateUserController;
    }

    public async Task<UserEntity> getUserById(string userId)
    {
        return await daoFactory.userDao!.getById(userId);
    }

    public async Task<string> getNextCloudGroupByUser(UserEntity entity)
    {
        return await updateUserController.getNextCloudGroup(entity);
    }

    public async Task<List<RoleEntity>> getRolesList()
    {
        return await daoFactory.roleDao!.getAll();
    }
}