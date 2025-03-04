using wsmcbl.src.model.config;

namespace wsmcbl.src.dto.config;

public static class DtoMapper
{
    public static LoginDto mapToDto(this string token) => new(token);
    
    public static UserDto mapToDto(this UserEntity user, string nextcloudGroup) => new(user, nextcloudGroup);

    public static RoleDto mapToDto(this RoleEntity value) => new(value);
    
    public static List<UserToListDto> mapToListDto(this List<UserEntity> value)
        => value.Select(e => new UserToListDto(e)).ToList();
    
    public static List<BasicPermissionDto> mapToListDto(this List<PermissionEntity> value)
        => value.Select(e => new BasicPermissionDto(e)).ToList();
    
    public static List<BasicRoleDto> mapToListDto(this List<RoleEntity> value)
        => value.Select(e => new BasicRoleDto(e)).ToList();

}