using wsmcbl.src.dto;
using wsmcbl.src.dto.input;
using wsmcbl.src.dto.secretary;

namespace wsmcbl.tests.utilities;

public static class TestDtoInputGenerator
{

    public static TransactionDto aTransactionDto()
    {
        return new TransactionDto
        {
            cashierId = "csh1",
            studentId = "std-1",
            dateTime = DateTime.Now,
            details = []
        };
    }
    
    public static SubjectToAssignDto aSubjectEnrollDto()
    {
        return new SubjectToAssignDto
        {
            subjectId = "sub001",
            teacherId = "tch001"
        };
    }
    
    public static DetailDto aDetailDto()
    {
        return new DetailDto
        {
            tariffId = 10,
            amount = 700,
            applyArrear = true
        };
    }
    
    
    public static DegreeToCreateDto aGradeDto()
    {
        return new DegreeToCreateDto()
        {
            label = "5to",
            schoolYear = "sch01",
            modality = "primaria",
            subjects = []
        };
    }
    
    public static SubjectInputDto aSubjectDto()
    {
        return new SubjectInputDto
        {
            name = "Lengua y Literatura",
            isMandatory = true,
            semester = 2
        };
    }
    
    public static TariffToCreateDto aTariffDto()
    {
        return new TariffToCreateDto
        {
            schoolYear = "sch001",
            amount = 700,
            concept = "Pago febrero",
            type = 1,
            modality = 1,
            dueDate = new DateOnlyDto(2024,1,1)
        };
    }
    
    public static EnrollmentToCreateDto aEnrollmentToCreateDto()
    {
        return new EnrollmentToCreateDto
        {
            degreeId = "gd01",
            quantity = 40
        };
    }
    
    public static SchoolYearToCreateDto aSchoolYearToCreateDto()
    {
        return new SchoolYearToCreateDto
        {
            degrees = [],
            tariffs = []
        };
    }

    public static EnrollmentToUpdateDto aEnrollmentDto()
    {
        return new EnrollmentToUpdateDto
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
            dueDate = new DateOnlyDto(2024,10,1)
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

    public static StudentFullDto aStudentFull()
    {
        throw new NotImplementedException();
    }
}