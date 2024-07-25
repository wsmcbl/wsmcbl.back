using wsmcbl.src.model.accounting;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.input;

public static class DtoMapper
{
    public static List<DebtHistoryEntity> toEntity(this IEnumerable<DetailDto> listDto)
    {
        return listDto.Where(i => !i.applyArrear)
            .Select(item => new DebtHistoryEntity{tariffId = item.tariffId})
            .ToList();
    }

    public static List<StudentParentEntity> toEntity(this IEnumerable<StudentParentDto> list)
    {
        return list.Select(item => item.toEntity()).ToList();
    }
}