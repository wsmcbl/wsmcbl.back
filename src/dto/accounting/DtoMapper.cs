using wsmcbl.src.model.accounting;

namespace wsmcbl.src.dto.accounting;

public static class DtoMapper
{
    public static List<DebtHistoryEntity> toEntity(this IEnumerable<TransactionDetailDto> listDto)
        => listDto.Where(i => !i.applyArrears)
            .Select(item => item.toDebtEntity()).ToList();
    
    private static TariffDto mapToDto(this TariffEntity value) => new(value);
    public static TransactionDto mapToDto(this TransactionEntity value) => new(value);
    public static StudentToListDto mapToDto(this StudentEntity value) => new(value);
    public static PaymentItemDto mapToDto(this DebtHistoryEntity entity) => new(entity);
    public static CreateStudentProfileDto mapToDto(this model.secretary.StudentEntity student, int modality)
        => new(student, modality);
    private static TransactionReportDto mapToDto(this TransactionReportView value)
        => new(value);

    private static TransactionToListDto mapToListDto(this TransactionReportView value) => new(value);
     
    
    public static List<BasicStudentDto> mapListTo(this IEnumerable<model.secretary.StudentView> value)
        => value.Select(e => new BasicStudentDto(e)).ToList();
    
    public static List<TariffDto> mapToListDto(this IEnumerable<TariffEntity> value)
        => value.Select(e => e.mapToDto()).ToList();
    
    public static List<TransactionReportDto> mapToListDto(this IEnumerable<TransactionReportView> value)
        => value.Select(e => e.mapToDto()).ToList();
    
    public static List<TransactionToListDto> mapToTransactionListDto(this IEnumerable<TransactionReportView> value)
        => value.Select(e => e.mapToListDto()).ToList();

    public static List<DebtDto> mapToListDto(this IEnumerable<DebtHistoryEntity> value)
        => value.Select(e => new DebtDto(e)).ToList();
    
    public static List<BasicStudentToEnrollDto> mapToListBasicEnrollDto(this IEnumerable<StudentEntity> list)
        => list.Select(e => new BasicStudentToEnrollDto(e)).ToList();
}