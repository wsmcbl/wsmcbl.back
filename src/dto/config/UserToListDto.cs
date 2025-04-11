using wsmcbl.src.model.config;

namespace wsmcbl.src.dto.config;

public class UserToListDto
{
    public Guid userId { get; set; }
    public string fullName { get; set; }
    public int roleId { get; set; }
    public bool isActive { get; set; }

    public UserToListDto(UserEntity user)
    {
        userId = (Guid)user.userId!;
        fullName = user.fullName();
        roleId = user.roleId;
        isActive = user.isActive;
    }
}