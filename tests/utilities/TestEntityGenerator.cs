using wsmcbl.src.model.accounting;
using wsmcbl.src.model.config;
using SecretaryStudentEntity = wsmcbl.src.model.secretary.StudentEntity;

namespace wsmcbl.tests.utilities;

public class TestEntityGenerator
{
    private UserEntity? _userEntity;
    private TariffEntity? _tariffEntity;
    private StudentEntity? _studentEntity;
    private CashierEntity? _cashierEntity;
    private DiscountEntity? _discountEntity;
    private DebtHistoryEntity? _debtHistoryEntity;
    private TransactionEntity? _transactionEntity;
    private SecretaryStudentEntity? _secretaryStudent;
    private TransactionTariffEntity? _transactionTariffEntity;


    private List<TariffEntity>? _aTariffList;
    private List<StudentEntity>? _aStudentList;
    private List<TariffTypeEntity>? _aTariffTypeList;
    private List<DebtHistoryEntity>? _aDebtHistoryList;
    private List<SecretaryStudentEntity>? _aSecretaryStudentList;


    public StudentEntity aStudent(string studentId)
    {
        _discountEntity ??= new DiscountEntity
        {
            discountId = 1,
            amount = 0.1f,
            description = "Description",
            tag = "A"
        };

        _studentEntity ??= new StudentEntity
        {
            student = aSecretaryStudent(studentId),
            discount = _discountEntity,
            transactions = new List<TransactionEntity>
            {
                aTransaction(studentId,
                [
                    new TransactionTariffEntity
                    {
                        amount = 2,
                        tariffId = aTariff().tariffId,
                        transactionId = "w"
                    }
                ])
            }
        };

        _studentEntity.studentId = studentId;

        return _studentEntity;
    }

    private SecretaryStudentEntity aSecretaryStudent(string studentId)
    {
        return _secretaryStudent ??= new SecretaryStudentEntity
        {
            studentId = studentId,
            name = "name-v",
            secondName = "sn",
            surname = "surname-v",
            secondSurname = "ssn",
            tutor = "tutor1",
            schoolYear = "2024",
            enrollmentLabel = "7mo"
        };
    }

    public TariffEntity aTariff()
    {
        return _tariffEntity ??= new TariffEntity
        {
            tariffId = 10,
            amount = 700,
            concept = "The concept",
            dueDate = new DateOnly(),
            isLate = true,
            schoolYear = DateTime.Now.Year.ToString(),
            type = 1
        };
    }

    public TransactionEntity aTransaction(string studentId, List<TransactionTariffEntity> detail)
    {
        return _transactionEntity ??= new TransactionEntity
        {
            transactionId = "tst-1",
            cashierId = "e",
            date = new DateTime(2024, 7, 10, 1, 1, 1, DateTimeKind.Utc),
            studentId = studentId,
            total = 700,
            details = detail
        };
    }

    public UserEntity aUser(string userId)
    {
        return _userEntity ??= new UserEntity
        {
            userId = userId,
            name = "name-v",
            secondName = "sn",
            surname = "surname-v",
            secondsurName = "ssn",
            username = "username-1",
            password = "12345-password"
        };
    }

    public CashierEntity aCashier(string cashierId)
    {
        return _cashierEntity ??= new CashierEntity
        {
            cashierId = cashierId,
            userId = "user-1",
            user = aUser("user-1")
        };
    }

    public DebtHistoryEntity aDebtHistory(string studentId)
    {
        return _debtHistoryEntity ??= new DebtHistoryEntity
        {
            studentId = studentId,
            tariffId = aTariff().tariffId,
            tariff = aTariff(),
            schoolyear = DateTime.Now.Year.ToString(),
            isPaid = false,
            debtBalance = 10,
            arrear = 10,    
            subAmount = 110,
            amount = 100
        };
    }


    public TransactionTariffEntity aTransactionTariffEntity()
    {
        _transactionTariffEntity ??= new TransactionTariffEntity
        {
            transactionId = "tst-1",
            tariffId = 10,
            amount = 700,
        };

        _transactionTariffEntity.setTariff(aTariff());
        
        return _transactionTariffEntity;
    }
    


    public List<StudentEntity> aStudentList() => _aStudentList ??= [aStudent("id1"), aStudent("id2")];

    public List<SecretaryStudentEntity> aSecretaryStudentList()
        => _aSecretaryStudentList ??= [aSecretaryStudent("id1"), aSecretaryStudent("id2")];

    public List<TariffEntity> aTariffList()
    {
        var tariff1 = aTariff();

        return _aTariffList ??= [tariff1];
    }

    public List<TariffTypeEntity> aTariffTypeList()
    {
        _aTariffTypeList ??=
        [
            new TariffTypeEntity
            {
                typeId = 1,
                description = "description 1"
            },

            new TariffTypeEntity
            {
                typeId = 2,
                description = "description aslk"
            }
        ];

        return _aTariffTypeList;
    }

    public (TransactionEntity, StudentEntity, CashierEntity, float[]) aTupleInvoice()
    {
        return (aTransaction("std-1", []), aStudent("std-1"), aCashier("csh-1"), [1.1f, 1]);
    }

    public List<DebtHistoryEntity> aDebtHistoryList(string studentId)
    {
        return _aDebtHistoryList ??= [aDebtHistory(studentId)];
    }
}