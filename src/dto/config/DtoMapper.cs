using wsmcbl.src.model.config;

namespace wsmcbl.src.dto.config;

public static class DtoMapper
{
    public static LoginDto mapToDto(this string token) => new(token);
    public static UserToCreateDto mapToCreateDto(this UserEntity user) => new(user);
    public static UserDto mapToDto(this UserEntity user) => new(user);
    public static List<UserToListDto> mapToListDto(this List<UserEntity> value)
        => value.Select(e => new UserToListDto(e)).ToList();
}