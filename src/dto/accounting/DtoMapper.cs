using wsmcbl.src.model.accounting;
using StudentEntity = wsmcbl.src.model.secretary.StudentEntity;

namespace wsmcbl.src.dto.accounting;

public static class DtoMapper
{
    public static List<DebtHistoryEntity> toEntity(this IEnumerable<DetailDto> listDto)
    {
        return listDto.Where(i => !i.applyArrear)
            .Select(item => new DebtHistoryEntity{tariffId = item.tariffId})
            .ToList();
    }

    public static CreateStudentProfileDto mapToDto(this StudentEntity student, int modality) => new(student, modality);
    
}