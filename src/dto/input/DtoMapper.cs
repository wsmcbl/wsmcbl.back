using wsmcbl.src.model.academy;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.secretary;
using secretary_StudentEntity = wsmcbl.src.model.secretary.StudentEntity;

namespace wsmcbl.src.dto.input;

public static class DtoMapper
{

    private static TransactionEntity? transaction;
    private static List<DebtHistoryEntity>? debtHistoryList;


    private static TransactionTariffEntity toEntity(this DetailDto dto)
    {
        return new TransactionTariffEntity
        {
            tariffId = dto.tariffId,
            amount = dto.amount
        };
    }

    public static TransactionEntity toEntity(this TransactionDto dto)
    {
        if (transaction != null)
            return transaction;

        transaction = new TransactionEntity
        {
            studentId = dto.studentId,
            cashierId = dto.cashierId,
            date = dto.dateTime,
            total = 0
        };

        foreach (var item in dto.details!)
        {
            transaction.details.Add(item.toEntity());
        }

        return transaction;
    }

    public static List<DebtHistoryEntity> toEntity(this IEnumerable<DetailDto> listDto)
    {
        if (debtHistoryList != null)
            return debtHistoryList;

        debtHistoryList = listDto
            .Where(i => !i.applyArrear).Select(item => new DebtHistoryEntity{tariffId = item.tariffId})
            .ToList();

        return debtHistoryList;
    }

    public static secretary_StudentEntity toEntity(this StudentDto dto)
    {
        return new secretary_StudentEntity
        {
            name = dto.name,
            secondName = dto.secondName,
            surname = dto.surname,
            secondSurname = dto.secondSurname,
            sex = dto.sex,
            birthday = dto.birthday.toDateOnly(),
            tutor = dto.tutor
        };
    }

    public static GradeEntity toEntity(this GradeToCreateDto dto)
    {
        var entity = new GradeEntity();
        entity.init(dto.label, dto.schoolYear, dto.modality);
        return entity;
    }

    public static GradeEntity toEntity(this GradeDto dto)
    {
        var entity = new GradeEntity();
        entity.setGradeId(dto.gradeId);
        entity.init(dto.label!, dto.schoolYear!, dto.modality!);
        return entity;
    }

    public static EnrollmentEntity toEntity(this EnrollmentDto dto)
    {
        var enrollment = new EnrollmentEntity
        {
            enrollmentId = dto.enrollmentId!
        };
        
        enrollment.updateData(dto.label!, dto.schoolYear!, dto.section!, dto.capacity);
        
        return enrollment;
    }
    
    private static DateOnly toDateOnly(this DateDto dto)
    {
        return new DateOnly(dto.year, dto.month, dto.day);
    }
}