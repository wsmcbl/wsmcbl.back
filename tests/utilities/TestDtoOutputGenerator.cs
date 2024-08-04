using wsmcbl.src.dto.output;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.config;
using wsmcbl.src.model.secretary;
using StudentEntity = wsmcbl.src.model.accounting.StudentEntity;

namespace wsmcbl.tests.utilities;

public class TestDtoOutputGenerator
{
    private DetailDto? _detailDto;
    private PaymentItemDto? _paymentItemDto;
    private InvoiceDto? _invoiceDto;

    public static List<DegreeEntity> aGradeList()
    {
        return
        [
            new DegreeEntity
            {
                degreeId = "gr01",
                label = "1ro",
                modality = "primaria",
                schoolYear = "sch001",
                subjectList = [],
                enrollments = []
            }
        ];
    }

    public static List<TeacherEntity> aTeacherList()
    {
        return
        [
            new TeacherEntity
            {
                teacherId = "tc1",
                enrollmentId = "enr1",
                userId = "us001",
                isGuide = false,
                user = new UserEntity
                {
                    name = "Juan",
                    surname = "Perez"
                }
            }
        ];
    }
    
    public static List<SchoolYearEntity> aSchoolYearList()
    {
        return
        [
            new SchoolYearEntity()
            {
                id = "sch1",
                label = "2024",
                isActive = true,
                startDate = new DateOnly(2024, 1, 1),
                deadLine = new DateOnly(2024, 12, 1)
            }
        ];
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
            schoolYear = entity.tariff.schoolYear,
            arrear = entity.arrear,
            subTotal = entity.amount,
            debtBalance = entity.amount - entity.debtBalance
        };

        _paymentItemDto.setDiscount(entity.subAmount);
       
        return _paymentItemDto;
    }

    private static BasicStudentDto aStudentBasicDto()
    {
        var entity = TestEntityGenerator.aStudent("std-1");
        
        return new BasicStudentDto
        {
            studentId = entity.studentId!,
            fullName = entity.fullName(),
            enrollmentLabel = entity.enrollmentLabel!,
            schoolyear = entity.schoolYear,
            tutor = entity.tutor!,
        };
    }
    
    public static List<BasicStudentDto> aStudentBasicDtoList()
    {
        return [aStudentBasicDto()];
    }
}
