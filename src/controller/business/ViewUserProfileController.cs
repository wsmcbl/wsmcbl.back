using wsmcbl.src.model.config;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class ViewUserProfileController : BaseController
{
    private AssignPermissionsController assignPermissionsController { get; }

    public ViewUserProfileController(DaoFactory daoFactory, AssignPermissionsController assignPermissionsController) :
        base(daoFactory)
    {
        this.assignPermissionsController = assignPermissionsController;
    }

    public async Task<UserEntity> getUserById(string userId)
    {
        return await daoFactory.userDao!.getById(userId);
    }

    public async Task<string> getNextCloudGroupByUser(UserEntity entity)
    {
        return await assignPermissionsController.getNextCloudGroup(entity);
    }
}