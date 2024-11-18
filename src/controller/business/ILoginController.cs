using wsmcbl.src.model.config;

namespace wsmcbl.src.controller.business;

public interface ILoginController
{
    public Task<string> getTokenByCredentials(UserEntity user);
    public Task<UserEntity> createUser(UserEntity user);
    public Task<UserEntity> getUserById(string userId);
    public Task<UserEntity> updateUser(string userId, UserEntity user);
}