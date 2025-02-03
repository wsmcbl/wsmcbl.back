using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.management;

public static class DtoMapper
{
    public static List<PartialDto> mapListToDto(this IEnumerable<PartialEntity> partialList) =>
        partialList.Select(e => new PartialDto(e)).ToList();
}