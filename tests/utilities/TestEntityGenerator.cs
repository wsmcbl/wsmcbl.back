using wsmcbl.src.dto.input;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.config;
using DetailDto = wsmcbl.src.dto.input.DetailDto;
using SecretaryStudentEntity = wsmcbl.src.model.secretary.StudentEntity;

namespace wsmcbl.tests.utilities;

public class TestEntityGenerator
{
    private SecretaryStudentEntity? _secretaryStudent;
    private SecretaryStudentEntity aSecretaryStudent(string studentId)
    {
        return _secretaryStudent ??= new SecretaryStudentEntity
        {
            studentId = studentId, 
            name = "name_1", 
            surname = "surname1", 
            tutor = "tutor1", 
            schoolYear = "2024", 
            enrollmentLabel = "7mo"
        };
    }

    private StudentEntity? _studentEntity;
    private DiscountEntity? discount;
    public StudentEntity aStudent(string studentId)
    {
        discount ??= new DiscountEntity
        {
            discountId = 1,
            amount = 1000,
            description = "Description",
            tag = "A"
        };

        _studentEntity ??= new StudentEntity
        {
            student = aSecretaryStudent(studentId),
            discount = discount
        };

        _studentEntity.studentId = studentId;

        return _studentEntity;
    }

    
    private List<StudentEntity>? _aStudentList;
    public List<StudentEntity> aStudentList() => _aStudentList ??= [aStudent("id1"), aStudent("id2")];


    private List<SecretaryStudentEntity>? _aSecretaryStudentList;
    public List<SecretaryStudentEntity> aSecretaryStudentList() => _aSecretaryStudentList ??= [aSecretaryStudent("id1"), aSecretaryStudent("id2")];


    private List<TariffEntity>? _aTariffList;
    public List<TariffEntity> aTariffList()
    {
        var tariff1 = new TariffEntity
        {
            tariffId = 2,
            amount = 1000,
            concept = "The concept",
            dueDate = new DateOnly(),
            isLate = true,
            schoolYear = "2024",
            type = 1
        };

        var tariff2 = tariff1;
        
        tariff2.tariffId = 3;
        tariff2.amount = 800;
        tariff2.type = 2;

        return _aTariffList ??= [tariff1, tariff2];
    }

    public List<TariffTypeEntity> aTariffTypeList()
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

    public (TransactionEntity, StudentEntity, CashierEntity, float[]) aTupleInvoice()
    {
        return (new TransactionEntity(), aStudent("std-1"), new CashierEntity{user = new UserEntity()}, [1.1f, 1]);
    }
    
    
    
    

    public TransactionDto aTransactionDto()
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
}