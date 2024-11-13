using wsmcbl.src.model.config;

namespace wsmcbl.src.controller.business;

public interface ILoginController
{
    public Task<string> getTokenByCredentials(UserEntity user);
}