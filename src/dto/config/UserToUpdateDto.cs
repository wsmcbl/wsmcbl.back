using System.ComponentModel.DataAnnotations;
using wsmcbl.src.exception;
using wsmcbl.src.model.config;

namespace wsmcbl.src.dto.config;

public class UserToUpdateDto
{
    [Required] public string name { get; set; } = null!;
    public string? secondName { get; set; }
    [Required] public string surname { get; set; } = null!;
    public string? secondSurname { get; set; }
    public List<int> permissionList { get; set; } = null!;
    public string nextCloudGroup { get; set; } = null!;
    
    public UserToUpdateDto(UserEntity user)
    {
        name = user.name;
        secondName = user.secondName;
        surname = user.surname;
        secondSurname = user.secondSurname;
    }
    
    public UserEntity toEntity(string userId)
    {
        if (Guid.TryParse(userId, out var userIdGuid))
        {
            return new UserEntity.Builder(userIdGuid)
                .setName(name)
                .setSecondName(secondName)
                .setSurname(surname)
                .setSecondSurname(secondSurname)
                .build();
        }

        throw new BadRequestException("UserId is not a valid.");
    }
}