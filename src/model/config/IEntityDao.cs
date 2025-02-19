using wsmcbl.src.model.dao;

namespace wsmcbl.src.model.config;

public interface IUserDao : IGenericDao<UserEntity, Guid>
{
    public Task<PagedResult<UserEntity>> getAll(PagedRequest request);
    public Task<UserEntity> getById(string userId);
    public Task<UserEntity> getUserByEmail(string email);
    public Task<bool> isEmailDuplicate(string email);
    public Task isUserDuplicate(UserEntity user);
}

public interface IMediaDao : IGenericDao<MediaEntity, int>
{
    public Task<string> getByTypeAndSchoolyear(int type, string schoolyearId);
}

public interface IPermissionDao : IGenericDao<PermissionEntity, int>
{
    public Task checkListId(List<int> permissionIdList);
}

public interface IUserPermissionDao : IGenericDao<UserPermissionEntity, string>;

public interface IRoleDao : IGenericDao<RoleEntity, int>;