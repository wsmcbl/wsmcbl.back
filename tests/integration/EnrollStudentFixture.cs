using Microsoft.EntityFrameworkCore;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.secretary;
using wsmcbl.tests.utilities;
using StudentEntity = wsmcbl.src.model.secretary.StudentEntity;

namespace wsmcbl.tests.integration;

public class EnrollStudentFixture : BaseFixture
{
    private StudentEntity? student { get; set; }

    protected override async Task seedData(DbContext context)
    {
        await context.Database.ExecuteSqlRawAsync("select cleanDatabase();");
        
        var schoolyear = TestEntityGenerator.aSchoolYear();
        schoolyear.id = "sch11";
        
        context.Add(schoolyear);
        await context.SaveChangesAsync();
        
        var degree = TestEntityGenerator.aDegree("degree");
        degree.schoolYear = schoolyear.id!;
        degree.subjectList = [];
        
        var enrollment = TestEntityGenerator.aEnrollment();
        enrollment.schoolYear = schoolyear.id!;
        enrollment.degreeId = degree.degreeId!;
        
        context.Add(degree);
        context.Add(enrollment);
        await context.SaveChangesAsync();

        var area = new SubjectAreaEntity
        {
            name = "d"
        };
        context.Add(area);
        await context.SaveChangesAsync();
        
        var subject = TestEntityGenerator.aSubject();
        subject.degreeId = degree.degreeId;
        subject.areaId = area.areaId;
        context.Add(subject);

        await seedTariff(context, schoolyear.id!);
        await seedStudent(context);

        await createAccountingStudent(student!, context);

        var debt = await context
            .Set<DebtHistoryEntity>()
            .FirstOrDefaultAsync(e => e.studentId == student!.studentId);

        debt!.debtBalance = debt.subAmount;

        context.Update(debt);
        await context.SaveChangesAsync();
    }

    private static async Task createAccountingStudent(StudentEntity studentEntity, DbContext context)
    {
        var accountingStudent = new src.model.accounting.StudentEntity
        {
            studentId = studentEntity.studentId,
            discountId = 1,
            educationalLevel = 1,
            enrollmentLabel = null
        };

        context.Add(accountingStudent);
        await context.SaveChangesAsync();
    }

    public StudentEntity getStudent() => student!;

    private async Task seedStudent(DbContext context)
    {
        var discount = new DiscountEntity()
        {
            discountId = 1,
            description = "Nada"
        };

        context.Add(discount);
        await context.SaveChangesAsync();
        
        var tutor = TestEntityGenerator.aTutor();
        context.Add(tutor);
        await context.SaveChangesAsync();
        
        student = TestEntityGenerator.aStudent("");
        student.studentId = null;
        student.tutorId = tutor.tutorId!;
        
        context.Add(student);
        await context.SaveChangesAsync();

        student.tutor = tutor;
    }

    private static async Task seedTariff(DbContext context, string schoolyearId)
    {
        var tariffType = new TariffTypeEntity
        {
            typeId = 2,
            description = "Matrícula",
        };

        context.Add(tariffType);
        await context.SaveChangesAsync();
        
        var tariff = TestEntityGenerator.aTariff();
        tariff.type = 2;
        tariff.schoolYear = schoolyearId;

        context.Add(tariff);
        await context.SaveChangesAsync();
    }
}