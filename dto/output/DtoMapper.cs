using wsmcbl.back.model.accounting;

namespace wsmcbl.back.dto.output;

public static class DtoMapper
{
    public static StudentDto mapToDto(this StudentEntity student)
    {
        var dto = new StudentDto
        {
            studentId = student.studentId!,
            fullName = student.fullName(),
            enrollmentLabel = student.enrollmentLabel,
            schoolYear = student.schoolYear,
            tutor = student.tutor,
            discount = student.discount!.amount,
            isActive = student.isActive,
            transactions = new List<TransactionDto>()
        };

        foreach (var item in student.transactions!)
        {
            dto.transactions.Add(item.mapToDto());
        }

        return dto;
    }

    private static StudentBasicDto mapToBasicDto(this StudentEntity student)
    {
        return new StudentBasicDto
        {
            studentId = student.studentId!,
            fullName = student.fullName(),
            enrollmentLabel = student.enrollmentLabel!,
            schoolyear = student.schoolYear,
            tutor = student.tutor!
        };
    }
    
    public static List<StudentBasicDto> mapToDto(this IEnumerable<StudentEntity> students)
    {
        return students.Select(student => student.mapToBasicDto()).ToList();
    }
    
    public static InvoiceDto mapToDto(this TransactionEntity transaction, StudentEntity? student, CashierEntity? cashier)
    {
        
        var element = new InvoiceDto
        {
            transactionId = transaction.transactionId!,
            cashierName = cashier!.fullName(),
            studentId = transaction.studentId,
            studentName = student!.fullName(),
            total = transaction.total,
            dateTime = transaction.date,
            tariffs = new List<DetailDto>()
        };

        foreach (var item in transaction.details)
        {
            element.tariffs.Add(item.mapToDto());
        }

        return element;
    }

    private static DetailDto mapToDto(this TransactionTariffEntity entity)
    {
        return new DetailDto
        {
            tariffId = entity.tariffId,
            //arrears = entity.arrears,
            //discount = entity.discount,
            //subTotal = entity.subTotal,
            concept  = entity.concept(),
            amount = entity.officialAmount(),
            itPaidLate = entity.itPaidLate(),
            schoolYear = entity.schoolYear()
        };
    }

    private static TransactionDto mapToDto(this TransactionEntity entity)
    {
        var t = new TransactionDto
        {
            transactionId = entity.transactionId,
            cashierId = entity.cashierId,
            date = entity.date,
            total = entity.total,
            details = new List<DetailDto>()
        };

        foreach (var item in entity.details)
        {
            t.details.Add(item.mapToDto());
        }

        return t;
    }
}