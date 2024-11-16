using wsmcbl.src.model.config;

namespace wsmcbl.src.controller.business;

public interface ILoginController
{
    public Task<string> getTokenByCredentials(UserEntity user);
    public Task<UserEntity> createUser(UserEntity user);
}