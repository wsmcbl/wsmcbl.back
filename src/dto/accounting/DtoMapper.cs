using wsmcbl.src.model.accounting;

namespace wsmcbl.src.dto.accounting;

public static class DtoMapper
{
    public static List<DebtHistoryEntity> toEntity(this IEnumerable<TransactionDetailDto> listDto)
        => listDto.Where(i => !i.applyArrears)
            .Select(item => item.toDebtEntity()).ToList();
    
    public static CreateStudentProfileDto mapToDto(this model.secretary.StudentEntity student, int modality)
        => new(student, modality);
    public static PaymentItemDto mapToDto(this DebtHistoryEntity entity) => new(entity);
    
    public static AccountingStudentDto mapToDto(this StudentEntity value) => new(value);
    private static TariffDto mapToDto(this TariffEntity value) => new(value);
    public static TransactionDto mapToDto(this TransactionEntity value) => new(value);
    
    private static BasicStudentDto mapToBasicDto(this StudentEntity value) => new(value); 
    
    public static List<BasicStudentDto> mapListTo(this IEnumerable<StudentEntity> value)
        => value.Select(student => student.mapToBasicDto()).ToList();
    
    public static List<TariffDto> mapToListDto(this IEnumerable<TariffEntity> value)
        => value.Select(e => e.mapToDto()).ToList();
}