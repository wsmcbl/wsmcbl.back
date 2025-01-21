using wsmcbl.src.controller.service;
using wsmcbl.src.exception;
using wsmcbl.src.model.config;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class AssignPermissionsController : BaseController
{
    private readonly HttpClient httpClient;

    public AssignPermissionsController(DaoFactory daoFactory, HttpClient httpClient) : base(daoFactory)
    {
        this.httpClient = httpClient;
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

        await assignNextcloudGroup(user, nextCloudGroup);

        return user;
    }

    private async Task assignNextcloudGroup(UserEntity user, string nextCloudGroup)
    {
        var nextcloudUserCreator = new NextcloudUserCreator(httpClient);
        await nextcloudUserCreator.assignGroup(user.email, nextCloudGroup.Trim());
    }

    public async Task assignPermissions(List<int> permissionList, Guid userId)
    {
        if (permissionList.Count == 0)
        {
            return;
        }
        
        await daoFactory.permissionDao!.checkListId(permissionList);

        foreach (var item in permissionList)
        {
            var userPermission = new UserPermissionEntity
            {
                userId = userId,
                permissionId = item
            };

            daoFactory.userPermissionDao!.create(userPermission);
        }

        await daoFactory.execute();
    }
}