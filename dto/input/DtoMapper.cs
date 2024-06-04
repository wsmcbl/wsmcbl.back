using wsmcbl.back.model.accounting;
using StudentEntity = wsmcbl.back.model.secretary.StudentEntity;

namespace wsmcbl.back.dto.input;

public static class DtoMapper
{
    private static TransactionTariffEntity toEntity(this DetailDto dto)
    {
        return new TransactionTariffEntity
        {
            tariffId = dto.tariffId,
            discount = dto.discount,
            arrears = dto.arrears,
            subTotal = 0
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

    public static StudentEntity toEntity(this StudentDto dto)
    {
        return new StudentEntity
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