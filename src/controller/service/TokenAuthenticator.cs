using Microsoft.AspNetCore.Identity;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.service;

public class TokenAuthenticator
{
    private IPasswordHasher<StudentEntity> passwordHasher { get; }
    private StudentEntity student { get; }
    public TokenAuthenticator(StudentEntity entity, IPasswordHasher<StudentEntity> passwordHasher)
    {
        this.passwordHasher = passwordHasher;
        student = entity;
    }
    
    public bool authenticateStudent(string token)
    {
        var result = passwordHasher.VerifyHashedPassword(student, token, student.accessToken!);
        return result == PasswordVerificationResult.Success;
    }

    public string encodeToken()
    {
        return passwordHasher.HashPassword(student, student.accessToken!);
    }
}