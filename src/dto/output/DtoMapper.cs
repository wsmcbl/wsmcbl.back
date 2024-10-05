using wsmcbl.src.model.accounting;

namespace wsmcbl.src.dto.output;

public static class DtoMapper
{
    public static InvoiceDto mapToDto(this TransactionEntity transaction, StudentEntity student, CashierEntity cashier)
    {
        var element = new InvoiceDto
        {
            transactionId = transaction.transactionId!,
            cashierName = cashier.fullName(),
            studentId = transaction.studentId,
            studentName = student.fullName(),
            total = transaction.total,
            dateTime = transaction.date,
            detail = transaction.details.Select(t => t.mapToDto(student)).ToList()
        };

        return element;
    }

    public static DetailDto mapToDto(this TransactionTariffEntity entity, StudentEntity student)
    {
        var detail = new DetailDto
        {
            tariffId = entity.tariffId,
            concept  = entity.concept(),
            amount = entity.officialAmount(),
            arrears = entity.calculateArrear(),
            discount = student.calculateDiscount(entity.officialAmount()),
            itPaidLate = entity.itPaidLate(),
            schoolYear = entity.schoolYear()
        };
        
        return detail;
    }

    public static PaymentItemDto mapToDto(this DebtHistoryEntity entity) => new(entity);
    public static StudentDto mapToDto(this StudentEntity student) => new(student);

    private static BasicStudentDto mapToBasicDto(this StudentEntity student)
    {
        return new BasicStudentDto
        {
            studentId = student.studentId!,
            fullName = student.fullName(),
            enrollmentLabel = student.enrollmentLabel!,
            tutor = student.tutor!
        };
    }
    
    public static List<BasicStudentDto> mapListTo(this IEnumerable<StudentEntity> students)
    {
        return students.Select(student => student.mapToBasicDto()).ToList();
    }
}