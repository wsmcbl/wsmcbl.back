using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.management;

public static class DtoMapper
{
    public static List<PartialDto> mapListToDto(this List<PartialEntity> list) =>
        list.Select(e => new PartialDto(e)).ToList();
}