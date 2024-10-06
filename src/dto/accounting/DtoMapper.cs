using wsmcbl.src.model.accounting;

namespace wsmcbl.src.dto.accounting;

public static class DtoMapper
{
    public static List<DebtHistoryEntity> toEntity(this IEnumerable<TransactionDetailDto> listDto)
    {
        return listDto.Where(i => !i.applyArrears)
            .Select(item => item.toDebtEntity()).ToList();
    }
    
    
    public static InvoiceDetailDto mapToDto(this TransactionTariffEntity transaction, StudentEntity student)
        => new(transaction, student);
    
    public static CreateStudentProfileDto mapToDto(this model.secretary.StudentEntity student, int modality)
        => new(student, modality);
    
    public static InvoiceDto mapToDto(this TransactionEntity transaction, StudentEntity student, CashierEntity cashier)
        => new(transaction, student, cashier);

    public static PaymentItemDto mapToDto(this DebtHistoryEntity entity) => new(entity);
    public static AccountingStudentDto mapToDto(this StudentEntity student) => new(student);

    private static BasicStudentDto mapToBasicDto(this StudentEntity student) => new(student);
    
    public static List<BasicStudentDto> mapListTo(this IEnumerable<StudentEntity> students)
    {
        return students.Select(student => student.mapToBasicDto()).ToList();
    }
}