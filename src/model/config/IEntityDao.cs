using wsmcbl.src.model.dao;

namespace wsmcbl.src.model.config;

public interface IUserDao : IGenericDao<UserEntity, string>
{
    public Task<UserEntity> getUserByEmail(string email);
    public Task<bool> isEmailDuplicate(string email);
}