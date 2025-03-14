using wsmcbl.src.dto.accounting;
using wsmcbl.src.model.accounting;

namespace wsmcbl.tests.utilities;

public class TestDtoOutputGenerator
{
    private PaymentItemDto? _paymentItemDto;

    
    public PaymentItemDto aPaymentDto(DebtHistoryEntity entity)
    {
        if (_paymentItemDto != null)
            return _paymentItemDto;
        
        _paymentItemDto = new PaymentItemDto()
        {
            tariffId = entity.tariffId,
            concept  = entity.tariff.concept,
            amount = entity.tariff.amount,
            itPaidLate = entity.tariff.isLate,
            schoolYear = entity.tariff.schoolyearId!,
            arrears = entity.arrears,
            subTotal = entity.amount,
            debtBalance = entity.amount - entity.debtBalance
        };

        _paymentItemDto.setDiscount(entity.subAmount);
       
        return _paymentItemDto;
    }

    private static BasicStudentDto aStudentBasicDto()
    {
        var entity = TestEntityGenerator.aAccountingStudent("std-1");
        
        return new BasicStudentDto
        {
            studentId = entity.studentId!,
            fullName = entity.fullName(),
            enrollmentLabel = entity.enrollmentLabel!,
            tutor = entity.tutor!,
        };
    }
    
    public static List<BasicStudentDto> aStudentBasicDtoList()
    {
        return [aStudentBasicDto()];
    }
}
