using wsmcbl.src.dto.input;
using wsmcbl.src.dto.output;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.config;
using wsmcbl.src.model.secretary;
using DetailDto = wsmcbl.src.dto.output.DetailDto;
using StudentDto = wsmcbl.src.dto.input.StudentDto;
using StudentEntity = wsmcbl.src.model.accounting.StudentEntity;
using TariffDto = wsmcbl.src.dto.output.TariffDto;

namespace wsmcbl.tests.utilities;

public class TestDtoGenerator
{
    private DetailDto? _detailDto;
    private TariffDto? _tariffDto;
    private InvoiceDto? _invoiceDto;

    public static List<GradeEntity> aGradeList()
    {
        return
        [
            new GradeEntity
            {
                gradeId = "gr01",
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
    
    
    public static StudentDto aStudentDto()
    {
        return new StudentDto
        {
            name = "Juan",
            secondName = "Pedro",
            surname = "Peréz",
            secondSurname = "Lopez",
            sex = true,
            birthday = new DateOnlyDto
            {
                year = 2024,
                month = 1,
                day = 1
            },
            tutor = "Pedro López"
        };
    }
    
    
    public static SchoolYearToCreateDto aSchoolYearToCreateDto()
    {
        return new SchoolYearToCreateDto
        {
            grades = [],
            tariffs = []
        };
    }

    public static EnrollmentDto aEnrollmentDto()
    {
        return new EnrollmentDto
        {
            enrollmentId = "er001",
            capacity = 60,
            quantity = 30,
            section = "A",
            subjects = []
        };
    }

    public static TariffDataDto aTariffDataDto()
    {
        return new TariffDataDto
        {
            typeId = 1,
            amount = 800,
            concept = "Pago mensualidad enero",
            modality = 2,
            dueDate = new DateOnlyDto
            {
                year = 2024,
                month = 10,
                day = 1
            }
        };
    }
    
    public static SubjectDataDto aSubjectDataDto()
    {
        return new SubjectDataDto
        {
            gradeIntId = 1,
            name = "Lengua y Literatura",
            isMandatory = true,
            semester = 3
        };
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

    public static EnrollmentToCreateDto aEnrollmentToCreateDto()
    {
        return new EnrollmentToCreateDto
        {
            gradeId = "gd01",
            quantity = 40
        };
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

    private static StudentBasicDto aStudentBasicDto()
    {
        var entity = TestEntityGenerator.aStudent("std-1");
        
        return new StudentBasicDto
        {
            studentId = entity.studentId!,
            fullName = entity.fullName(),
            enrollmentLabel = entity.enrollmentLabel!,
            schoolyear = entity.schoolYear,
            tutor = entity.tutor!,
        };
    }
    
    public static List<StudentBasicDto> aStudentBasicDtoList()
    {
        return [aStudentBasicDto()];
    }
}
