using System.Runtime.InteropServices.JavaScript;
using wsmcbl.src.dto.input;
using wsmcbl.src.dto.output;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.config;
using DetailDto = wsmcbl.src.dto.input.DetailDto;

namespace wsmcbl.tests.unit.controller;

public static class EntityMaker
{
    private static src.model.secretary.StudentEntity getASecretaryStudent(string studentId)
    {
        return new src.model.secretary.StudentEntity
        {
            studentId = studentId,
            name = "name1",
            surname = "surname1",
            tutor = "tutor1",
            schoolYear = "2024",
            enrollmentLabel = "7mo"
        };
    }
    
    public static StudentEntity getAStudent(string studentId)
    {
        var discount = new DiscountEntity
        {
            discountId = 1,
            amount = 1000,
            description = "Description",
            tag = "A"
        };
        
        return new StudentEntity
        {
            studentId = studentId,
            student = getASecretaryStudent(studentId),
            discount = discount
        };
    }
    
    public static List<StudentEntity> getAStudentList() => [getAStudent("id1"), getAStudent("id2")];
    
    public static List<src.model.secretary.StudentEntity> getASecretaryStudentList() => [getASecretaryStudent("id1"), getASecretaryStudent("id2")];
    
    public static List<TariffEntity> getATariffList()
    {
        var tariff1 = new TariffEntity
        {
            tariffId = 2,
            amount = 1000,
            concept = "concept",
            dueDate = new DateOnly(),
            isLate = true,
            schoolYear = "2024",
            type = 1
        };
        
        var tariff2 = new TariffEntity
        {
            tariffId = 3,
            amount = 1000,
            concept = "concept kalks",
            dueDate = new DateOnly(),
            isLate = false,
            schoolYear = "2025",
            type = 3
        };

        return [tariff1, tariff2];
    }

    public static List<TariffTypeEntity> getTariffTypeList()
    {
        var tariffType1 = new TariffTypeEntity
        {
            typeId = 1,
            description = "description 1"
        };
        
        var tariffType2 = new TariffTypeEntity
        {
            typeId = 2,
            description = "description aslk"
        };

        return [tariffType1, tariffType2];
    }

    public static TransactionDto getATransactionDto()
    {
        var transactionDto = new TransactionDto
        {
            cashierId = "caj-ktinoco",
            studentId = "std-id",
            dateTime = DateTime.Now,
            details = new List<DetailDto>()
        };
        
        transactionDto.details.Add(new DetailDto
        {
            amount = 1000,
            applyArrear = true,
            tariffId = 1
        });

        return transactionDto;
    }

    public static TransactionDto getAIncorrectTransactionDto()
    {
        var transaction = getATransactionDto();
        transaction.cashierId = "";
        transaction.studentId = "";

        return transaction;
    }

    public static (TransactionEntity, StudentEntity, CashierEntity, float[]) getAInvoice()
    {
        return (new TransactionEntity(), new StudentEntity{student = getASecretaryStudent("std")}, new CashierEntity{user = new UserEntity()}, [1.1f, 1]);
    }
}