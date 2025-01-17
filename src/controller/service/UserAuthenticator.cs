using Microsoft.AspNetCore.Identity;
using wsmcbl.src.exception;
using wsmcbl.src.model.config;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.service;

public class UserAuthenticator
{
    private IUserDao userDao { get; }
    private IPasswordHasher<UserEntity> passwordHasher { get; }
    
    public UserAuthenticator(DaoFactory daoFactory, IPasswordHasher<UserEntity> passwordHasher)
    {
        userDao = daoFactory.userDao!;
        this.passwordHasher = passwordHasher;
    }

    public async Task<UserEntity> authenticateUser(UserEntity user)
    {
        var entity = await userDao.getUserByEmail(user.email);

        var result = passwordHasher.VerifyHashedPassword(entity, entity.password, user.password);
        if (result != PasswordVerificationResult.Success)
        {
            throw new IncorrectDataBadRequestException("User not authenticated.");
        }

        return entity;
    }

    public void encodePassword(UserEntity user, string password)
    {
        user.password = passwordHasher.HashPassword(user, password);
    }
}