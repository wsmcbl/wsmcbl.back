using wsmcbl.src.dto.output;
using wsmcbl.src.model.accounting;

namespace wsmcbl.tests.utilities;

public class TestDtoGenerator
{
    private DetailDto? _detailDto;
    private TariffDto? _tariffDto;
    private InvoiceDto? _invoiceDto;
    private StudentBasicDto? _studentBasicDto;
    private src.dto.input.TransactionDto? _transactionDto;

    private List<StudentBasicDto>? _studentBasicDtoList;


    public src.dto.input.TransactionDto aTransactionDto()
    {
        _transactionDto = new src.dto.input.TransactionDto
        {
            cashierId = "caj-ktinoco",
            studentId = "std-id",
            dateTime = DateTime.Now,
            details = new List<src.dto.input.DetailDto>()
        };

        _transactionDto.details.Add(new src.dto.input.DetailDto
        {
            amount = 1000,
            applyArrear = true,
            tariffId = 1
        });

        return _transactionDto;
    }
    
    public InvoiceDto aInvoiceDto()
    {
        return _invoiceDto ??= new InvoiceDto
        {
            transactionId = "tst-1",
            cashierName = "name-v sn surname-v ssn",
            studentId = "std-1",
            studentName = "name-v sn surname-v ssn",
            total = 700,
            dateTime = new DateTime(2024, 7, 10, 1, 1, 1, DateTimeKind.Utc),
            detail = []
        };
    }

    public DetailDto aDetailDto(TariffEntity tariff, StudentEntity student)
    {
        if (_detailDto != null)
            return _detailDto;
        
        _detailDto = new DetailDto
        {
            tariffId = tariff.tariffId,
            schoolYear = tariff.schoolYear,
            concept = tariff.concept,
            amount = tariff.amount,
            discount = student.calculateDiscount(tariff.amount),
            itPaidLate = tariff.isLate
        };
        
        _detailDto.arrears = (float)(_detailDto.itPaidLate ? _detailDto.amount * 0.1 : 0);

        return _detailDto;
    }

    public TariffDto aTariffDto(DebtHistoryEntity entity)
    {
        if (_tariffDto != null)
            return _tariffDto;
        
        _tariffDto = new TariffDto
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

        _tariffDto.setDiscount(entity.subAmount);
       
        return _tariffDto;
    }

    public StudentBasicDto aStudentBasicDto()
    {
        var entity = new TestEntityGenerator().aStudent("std-1");
        
        return _studentBasicDto ??= new StudentBasicDto
        {
            studentId = entity.studentId!,
            fullName = entity.fullName(),
            enrollmentLabel = entity.enrollmentLabel!,
            schoolyear = entity.schoolYear,
            tutor = entity.tutor!
        };
    }
    
    public List<StudentBasicDto> aStudentBasicDtoList()
    {
        return _studentBasicDtoList ??= [aStudentBasicDto()];
    }
}
