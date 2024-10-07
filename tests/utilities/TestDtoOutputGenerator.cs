using wsmcbl.src.dto.accounting;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.config;
using wsmcbl.src.model.secretary;
using StudentEntity = wsmcbl.src.model.accounting.StudentEntity;

namespace wsmcbl.tests.utilities;

public class TestDtoOutputGenerator
{
    private PaymentItemDto? _paymentItemDto;

    public static List<DegreeEntity> aGradeList()
    {
        return
        [
            new DegreeEntity
            {
                degreeId = "gr01",
                label = "1ro",
                educationalLevel = "primaria",
                schoolYear = "sch001",
                subjectList = [],
                enrollmentList = []
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
            schoolYear = entity.tariff.schoolYear!,
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
