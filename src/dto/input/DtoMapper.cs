using wsmcbl.src.model.accounting;

namespace wsmcbl.src.dto.input;

public static class DtoMapper
{
    public static List<DebtHistoryEntity> toEntity(this IEnumerable<DetailDto> listDto)
    {
        return listDto.Where(i => !i.applyArrear)
            .Select(item => new DebtHistoryEntity{tariffId = item.tariffId})
            .ToList();
    }
}