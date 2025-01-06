using wsmcbl.src.model.dao;

namespace wsmcbl.src.model.config;

public interface IUserDao : IGenericDao<UserEntity, Guid>
{
    public Task<UserEntity> getById(string userId);
    public Task<UserEntity> getUserByEmail(string email);
    public Task<bool> isEmailDuplicate(string email);
}

public interface IMediaDao : IGenericDao<MediaEntity, int>
{
    public Task<string> getByTypeAndSchoolyear(int type, string schoolyearId);
}

public interface IPermissionDao : IGenericDao<PermissionEntity, string>
{
    public Task<bool> checkListId(List<int> permissionIdList);
}