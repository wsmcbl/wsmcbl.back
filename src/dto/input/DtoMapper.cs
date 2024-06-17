using wsmcbl.src.model.accounting;
using secretary_StudentEntity = wsmcbl.src.model.secretary.StudentEntity;
using StudentEntity = wsmcbl.src.model.secretary.StudentEntity;

namespace wsmcbl.src.dto.input;

public static class DtoMapper
{
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
        var transaction = new TransactionEntity
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
    
    

    public static List<DebtHistoryEntity> toEntity(this IEnumerable<DetailDto> listDto, string studentId)
    {
        return listDto.Where(i => !i.applyArrear)
            .Select(item => new DebtHistoryEntity
            {
                studentId = studentId, 
                tariffId = item.tariffId
            })
            .ToList();
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
    private static DateOnly toDateOnly(this DateDto dto)
    {
        return new DateOnly(dto.year, dto.month, dto.day);
    }
}