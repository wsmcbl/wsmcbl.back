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
            discount = student.discount!.amount,
            itPaidLate = entity.itPaidLate(),
            schoolYear = entity.schoolYear()
        };

        detail.computeDiscount();
        detail.computeArrears();

        return detail;
    }

    public static TariffDto mapToDto(this DebtHistoryEntity entity)
    {
        var tariff = new TariffDto
        {
            tariffId = entity.tariffId,
            concept  = entity.tariff.concept,
            amount = entity.tariff.amount,
            itPaidLate = entity.tariff.isLate,
            schoolYear = entity.tariff.schoolYear,
            arrear = entity.arrear,
            subTotal = entity.amount,
            debtBalance = entity.amount - entity.debtBalance
        };

        tariff.setDiscount(entity.subAmount);
        
        return tariff;
    }
    
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
            paymentHistory = []
        };

        student.debtHistory ??= [];
        
        foreach (var item in student.debtHistory)
        {
            dto.paymentHistory.Add(item.mapToDto());
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
    
    public static List<StudentBasicDto> mapListToDto(this IEnumerable<StudentEntity> students)
    {
        return students.Select(student => student.mapToBasicDto()).ToList();
    }
}