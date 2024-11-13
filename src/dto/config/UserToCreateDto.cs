using wsmcbl.src.model.config;

namespace wsmcbl.src.dto.config;

public class UserToCreateDto
{
    public string name { get; set; } = null!;
    public string? secondName { get; set; }
    public string surname { get; set; } = null!;
    public string? secondSurname { get; set; }
    public string password { get; set; } = null!;
    public string? email { get; set; }
    
    
    public UserToCreateDto()
    {
    }

    public UserToCreateDto(UserEntity user)
    {
        name = user.name;
        secondName = user.secondName;
        surname = user.surname;
        secondSurname = user.secondsurName;
        email = user.email;
        password = "";
    }
    
    public UserEntity toEntity()
    {
        return new UserEntity
        {
            name = name,
            secondName = secondName,
            surname = surname,
            secondsurName = secondSurname,
            password = password,
            email = email
        };
    }
}