using wsmcbl.src.model.dao;

namespace wsmcbl.src.model.config;

public interface IUserDao : IGenericDao<UserEntity, Guid>
{
    public Task<PagedResult<UserEntity>> getPaginated(PagedRequest request);
    public Task<UserEntity> getById(string userId);
    public Task<UserEntity> getUserByEmail(string email);
    public Task<bool> isEmailAlreadyRegistered(string email);
    public Task checkForDuplicateUser(UserEntity user);
}

public interface IMediaDao : IGenericDao<MediaEntity, int>
{
    public Task<string> getByTypeIdAndSchoolyearId(int type, string schoolyearId);
}

public interface IPermissionDao : IGenericDao<PermissionEntity, int>
{
    public Task verifyIdListOrFail(List<int> permissionIdList);
}

public interface IUserPermissionDao : IGenericDao<UserPermissionEntity, string>;

public interface IRoleDao : IGenericDao<RoleEntity, int>;

public interface IRolePermissionDao : IGenericDao<RolePermissionEntity, int>;