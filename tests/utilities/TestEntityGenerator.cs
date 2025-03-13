using wsmcbl.src.model;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.config;
using wsmcbl.src.model.secretary;
using StudentEntity = wsmcbl.src.model.secretary.StudentEntity;
using AccountingStudentEntity = wsmcbl.src.model.accounting.StudentEntity;
using SubjectEntity = wsmcbl.src.model.secretary.SubjectEntity;

namespace wsmcbl.tests.utilities;

public class TestEntityGenerator
{
    private TransactionTariffEntity? _transactionTariffEntity;


    public static DegreeDataEntity aDegreeData()
    {
        return new DegreeDataEntity()
        {
            degreeDataId = 1,
            label = "4to",
            educationalLevel = 2,
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
            section = "A",
            tag = "02"
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
            educationalLevel = 1,
            dueDate = new DateOnly(2020, 1, 1)
        };
    }

    public static List<DegreeEntity> aDegreeList()
    {
        return [aDegree("gd-10")];
    }


    public static SchoolyearEntity aSchoolYear()
    {
        return new SchoolyearEntity
        {
            id = "sch001",
            label = DateTime.Now.Year.ToString(),
            isActive = true,
            deadLine = new DateOnly(2000, 1, 1),
            startDate = new DateOnly(2000, 12, 1)
        };
    }

    public static List<SchoolyearEntity> aSchoolYearList()
    {
        return [aSchoolYear()];
    }


    public static TeacherEntity aTeacher()
    {
        return new TeacherEntity()
        {
            teacherId = "tc-1",
            isGuide = true
        };
    }
    
    public static List<TeacherEntity> aTeacherList()
    {
        return [aTeacher()];
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
            initials = "L y L",
            areaId = 1
        };
    }


    public static DegreeEntity aDegree(string degreeId)
    {
        return new DegreeEntity
        {
            degreeId = degreeId,
            label = "11vo",
            educationalLevel = "secundaria",
            schoolyearId = "sch001",
            enrollmentList = [],
            subjectList = [aSubject()],
            tag = "01"
        };
    }

    public static AccountingStudentEntity aAccountingStudent(string studentId)
    {
        var discountEntity = new DiscountEducationalLevelEntity()
        {
            discountId = 1,
            amount = 0.1f
        };

        return new AccountingStudentEntity
        {
            studentId = studentId,
            student = aStudent(studentId),
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

    public static StudentEntity aStudent(string studentId)
    {
        var result = new StudentEntity.Builder()
            .setId(studentId)
            .setTutorId("tur-00")
            .setName("Jonas")
            .setSecondName("Alexander")
            .setSurname("Lopez")
            .setSecondSurname("Alvarez")
            .setReligion("Ninguna")
            .setAddress("Desconocida")
            .setDiseases("Ninguna")
            .build();

        var parent = new StudentParentEntity
        {
            name = "Madre del estudiante",
            sex = true,
            occupation = "Desempleado",
            idCard = "001-xxxxx"
        };
        
        result.parents = [parent];
        result.tutor = aTutor("tur-00");

        return result;
    }

    public static TariffEntity aTariff()
    {
        return new TariffEntity
        {
            tariffId = 10,
            amount = 1000,
            concept = "pago mes de abril",
            isLate = true,
            educationalLevel = 1,
            schoolYear = "sch001",
            type = 1,
            dueDate = new DateOnly(2024,1,1)
        };
    }
    
    public static TariffEntity aTariffNotMonthly()
    {
        return new TariffEntity
        {
            tariffId = 11,
            amount = 900,
            concept = "Pago excursion",
            isLate = true,
            educationalLevel = 1,
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

    public static UserEntity aUser()
    {
        return new UserEntity
        {
            name = "name-v",
            secondName = "sn",
            surname = "surname-v",
            secondSurname = "ssn",
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
            user = aUser()
        };
    }

    public static DebtHistoryEntity aDebtHistory(string studentId, bool isPaid = true)
    {
        return new DebtHistoryEntity
        {
            studentId = studentId,
            tariffId = aTariff().tariffId,
            tariff = aTariff(),
            schoolyear = "sch001",
            isPaid = isPaid,
            debtBalance = 10,
            arrears = 10,
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


    public static List<StudentView> aStudentList() =>
    [
        
    ];

    public static List<StudentEntity> aSecretaryStudentList() => [aStudent("id1"), aStudent("id2")];

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
                description = "description 2"
            }
        ];
    }

    public static (TransactionEntity, AccountingStudentEntity, CashierEntity, float[]) aTupleInvoice()
    {
        return (aTransaction("std-1", []), aAccountingStudent("std-1"), aCashier("csh-1"), [1.1f, 1]);
    }

    public static List<DebtHistoryEntity> aDebtHistoryList(string studentId, bool isPaid = true)
    {
        return [aDebtHistory(studentId, isPaid), aDebtHistoryNotMonthly(studentId)];
    }

    private static DebtHistoryEntity aDebtHistoryNotMonthly(string studentId)
    {
        return new DebtHistoryEntity
        {
            studentId = studentId,
            tariffId = aTariffNotMonthly().tariffId,
            tariff = aTariffNotMonthly(),
            schoolyear = "sch001",
            isPaid = true,
            debtBalance = 10,
            arrears = 10,
            subAmount = 110,
            amount = 100
        };
    }

    public static StudentTutorEntity aTutor(string? tutorId = null)
    {
        return new StudentTutorEntity("Juan Morales", "78451236", tutorId);
    }

    public static src.model.academy.StudentEntity aAcademyStudent(string studentId)
    {
        return new src.model.academy.StudentEntity
        {
            studentId = studentId,
            enrollmentId = aEnrollment().enrollmentId,
            schoolYear = aSchoolYear().id!,
            
        };
    }
}