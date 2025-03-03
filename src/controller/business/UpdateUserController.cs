using wsmcbl.src.controller.service;
using wsmcbl.src.exception;
using wsmcbl.src.model.config;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class UpdateUserController : BaseController
{
    private NextcloudUserCreator nextcloudUserCreator { get; set; }

    public UpdateUserController(DaoFactory daoFactory, HttpClient httpClient) : base(daoFactory)
    {
        nextcloudUserCreator = new NextcloudUserCreator(httpClient);
    }

    public async Task<List<PermissionEntity>> getPermissionList()
    {
        return await daoFactory.permissionDao!.getAll();
    }

    public async Task<UserEntity> updateUser(UserEntity value, string nextCloudGroup)
    {
        var user = await daoFactory.userDao!.getById(value.userId.ToString()!);
        if (user == null)
        {
            throw new EntityNotFoundException("UserEntity", value.userId!.ToString());
        }

        user.update(value);
        await daoFactory.execute();

        await assignNextcloudGroup(user, nextCloudGroup);

        return user;
    }

    private async Task assignNextcloudGroup(UserEntity user, string nextCloudGroup)
    {
        await nextcloudUserCreator.assignGroup(user.email, nextCloudGroup.Trim());
    }

    public async Task assignPermissions(UserEntity user, List<int> permissionList)
    {
        if (permissionList.Count == 0)
        {
            return;
        }

        await daoFactory.permissionDao!.checkListId(permissionList);
        
        foreach (var item in permissionList)
        {
            if (user.isAlreadyAssigned(item))
            {
                continue;
            }
            
            var userPermission = new UserPermissionEntity
            {
                userId = (Guid)user.userId!,
                permissionId = item
            };

            daoFactory.userPermissionDao!.create(userPermission);
        }

        await daoFactory.execute();
    }

    public async Task<string> getNextCloudGroup(UserEntity entity)
    {
        return await nextcloudUserCreator.getGroupByUserMail(entity.email);
    }

    public async Task<UserEntity> updateUserPassword(string userId)
    {
        throw new NotImplementedException();
    }
}