using wsmcbl.src.model.academy;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.config;
using wsmcbl.src.model.secretary;
using SecretaryStudentEntity = wsmcbl.src.model.secretary.StudentEntity;
using StudentEntity = wsmcbl.src.model.accounting.StudentEntity;
using SubjectEntity = wsmcbl.src.model.secretary.SubjectEntity;

namespace wsmcbl.tests.utilities;

public class TestEntityGenerator
{
    private TransactionTariffEntity? _transactionTariffEntity;


    public static DegreeDataEntity aGradeData()
    {
        return new DegreeDataEntity()
        {
            degreeDataId = 1,
            label = "4to",
            modality = 2,
            subjectList = [aSubjectData()]
        };
    }

    public static EnrollmentEntity aEnrollment()
    {
        return new EnrollmentEntity
        {
            enrollmentId = "en-1",
            degreeId = "gd1",
            capacity = 20,
            label = "A",
            quantity = 20,
            schoolYear = "sch22",
            section = "A"
        };
    }

    public static SubjectDataEntity aSubjectData()
    {
        return new SubjectDataEntity()
        {
            degreeDataId = 1,
            subjectDataId = 1,
            isMandatory = true,
            name = "Español",
            semester = 1
        };
    }

    public static TariffDataEntity aTariffData()
    {
        return new TariffDataEntity
        {
            tariffDataId = 1,
            typeId = 1,
            concept = "Pago mes de enero",
            amount = 1000,
            modality = 1,
            dueDate = new DateOnly(2020, 1, 1)
        };
    }

    public static List<DegreeEntity> aGradeList()
    {
        return [aGrade("gd-10")];
    }


    public static SchoolYearEntity aSchoolYear()
    {
        return new SchoolYearEntity
        {
            id = "sch001",
            label = DateTime.Now.Year.ToString(),
            isActive = true,
            deadLine = new DateOnly(2000, 1, 1),
            startDate = new DateOnly(2000, 12, 1)
        };
    }

    public static List<SchoolYearEntity> aSchoolYearList()
    {
        return [aSchoolYear()];
    }

    public static List<TeacherEntity> aTeacherList()
    {
        return
        [
            new TeacherEntity()
            {
                teacherId = "tc-1",
                enrollmentId = "en001",
                userId = "u001",
                isGuide = true
            }
        ];
    }


    public static SubjectEntity aSubject()
    {
        return new SubjectEntity
        {
            subjectId = "sub1",
            degreeId = "gd-1",
            isMandatory = true,
            name = "Español",
            semester = 3,
            initials = "L y L"
        };
    }


    public static DegreeEntity aGrade(string gradeId)
    {
        return new DegreeEntity
        {
            degreeId = gradeId,
            label = "11vo",
            modality = "secundaria",
            schoolYear = "sch001",
            enrollments = [],
            subjectList = [aSubject()]
        };
    }

    public static StudentEntity aStudent(string studentId)
    {
        var discountEntity = new DiscountEntity
        {
            discountId = 1,
            amount = 0.1f,
            description = "Description",
            tag = "A"
        };

        return new StudentEntity
        {
            studentId = studentId,
            student = aSecretaryStudent(studentId),
            discount = discountEntity,
            discountId = 1,
            enrollmentLabel = "",
            transactions = [aTransaction(studentId, [aTransactionTariff()])]
        };
    }

    public static TransactionTariffEntity aTransactionTariff()
    {
        return new TransactionTariffEntity
        {
            amount = 100,
            tariffId = aTariff().tariffId,
            transactionId = "tst-0001"
        };
    }

    private static SecretaryStudentEntity aSecretaryStudent(string studentId)
    {
        return new SecretaryStudentEntity
        {
            studentId = studentId,
            name = "name-v",
            secondName = "sn",
            surname = "surname-v",
            secondSurname = "ssn",
            schoolYear = "2024",
            religion = "Evangelico",
            address = "la direccion (desconcida)"
        };
    }

    public static TariffEntity aTariff()
    {
        return new TariffEntity
        {
            tariffId = 10,
            amount = 1000,
            concept = "pago mes de abril",
            isLate = true,
            modality = 1,
            schoolYear = "sch001",
            type = 1,
            dueDate = new DateOnly(2024,1,1)
        };
    }
    
    private static TariffEntity aTariffNotMensual()
    {
        return new TariffEntity
        {
            tariffId = 11,
            amount = 900,
            concept = "Pago excursion",
            isLate = true,
            modality = 1,
            schoolYear = "sch001",
            type = 2
        };
    }

    public static TransactionEntity aTransaction(string studentId, List<TransactionTariffEntity> detail)
    {
        return new TransactionEntity
        {
            transactionId = "tst-1",
            cashierId = "e",
            date = new DateTime(2024, 7, 10, 1, 1, 1, DateTimeKind.Utc),
            studentId = studentId,
            total = 700,
            details = detail
        };
    }

    public static UserEntity aUser(string userId)
    {
        return new UserEntity
        {
            userId = userId,
            name = "name-v",
            secondName = "sn",
            surname = "surname-v",
            secondsurName = "ssn",
            username = "username-1",
            password = "12345-password",
            isActive = true,
            email = "user@mail.com"
        };
    }

    public static CashierEntity aCashier(string cashierId)
    {
        return new CashierEntity
        {
            cashierId = cashierId,
            userId = "user-1",
            user = aUser("user-1")
        };
    }

    public static DebtHistoryEntity aDebtHistory(string studentId, bool isPaid)
    {
        return new DebtHistoryEntity
        {
            studentId = studentId,
            tariffId = aTariff().tariffId,
            tariff = aTariff(),
            schoolyear = "sch001",
            isPaid = isPaid,
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


    public static List<StudentEntity> aStudentList() =>
    [
        //aStudent("id1")
        new StudentEntity
        {
            studentId = "std-10",
            student = aSecretaryStudent("std-10"),
            discount = new DiscountEntity
            {
                discountId = 1,
                amount = 0.1f,
                description = "Description",
                tag = "A"
            },
            enrollmentLabel = "",
            transactions = new List<TransactionEntity>
            {
                new TransactionEntity
                {
                    transactionId = "tst-1",
                    cashierId = "e",
                    date = new DateTime(2024, 7, 10, 1, 1, 1, DateTimeKind.Utc),
                    studentId = "std-10",
                    total = 700,
                    details =
                    [
                        new TransactionTariffEntity
                            { amount = 2, tariffId = aTariff().tariffId, transactionId = "w" }
                    ]
                }
            }
        }
    ];

    public static List<SecretaryStudentEntity> aSecretaryStudentList() => [aSecretaryStudent("id1"), aSecretaryStudent("id2")];

    public static List<TariffEntity> aTariffList()
    {
        return [aTariff()];
    }

    public static List<TariffTypeEntity> aTariffTypeList()
    {
        return
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
    }

    public static (TransactionEntity, StudentEntity, CashierEntity, float[]) aTupleInvoice()
    {
        return (aTransaction("std-1", []), aStudent("std-1"), aCashier("csh-1"), [1.1f, 1]);
    }

    public static List<DebtHistoryEntity> aDebtHistoryList(string studentId, bool isPaid)
    {
        return [aDebtHistory(studentId, isPaid), aDebtHistoryNotMensual(studentId)];
    }

    private static DebtHistoryEntity aDebtHistoryNotMensual(string studentId)
    {
        return new DebtHistoryEntity
        {
            studentId = studentId,
            tariffId = aTariffNotMensual().tariffId,
            tariff = aTariffNotMensual(),
            schoolyear = "sch001",
            isPaid = true,
            debtBalance = 10,
            arrear = 10,
            subAmount = 110,
            amount = 100
        };
    }
}