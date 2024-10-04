using Microsoft.EntityFrameworkCore;
using wsmcbl.src.model.accounting;
using wsmcbl.tests.utilities;
using StudentEntity = wsmcbl.src.model.secretary.StudentEntity;

namespace wsmcbl.tests.integration;

public class EnrollStudentFixture : BaseFixture
{
    private StudentEntity? student;
    protected override async Task seedData(DbContext context)
    {
        var schoolyear = TestEntityGenerator.aSchoolYear();
        schoolyear.id = "sch11";
        
        context.Add(schoolyear);
        await context.SaveChangesAsync();
        
        var degree = TestEntityGenerator.aDegree("degree");
        degree.schoolYear = schoolyear.id!;
        
        var enrollment = TestEntityGenerator.aEnrollment();
        enrollment.schoolYear = schoolyear.id!;
        enrollment.degreeId = degree.degreeId!;
        
        context.Add(degree);
        context.Add(enrollment);
        await context.SaveChangesAsync();

        await seedTariff(context, schoolyear.id!);
        await seedStudent(context, schoolyear.id);

        var debt = await context
            .Set<DebtHistoryEntity>()
            .FirstOrDefaultAsync(e => e.studentId == student!.studentId);

        debt!.debtBalance = debt.subAmount;

        context.Update(debt);
        await context.SaveChangesAsync();
    }

    public StudentEntity getStudent() => student!;

    private async Task seedStudent(DbContext context, string schoolyearId)
    {
        var discount = new DiscountEntity()
        {
            discountId = 1,
            amount = 0,
            description = "Nada"
        };

        context.Add(discount);
        await context.SaveChangesAsync();
        
        student = TestEntityGenerator.aStudent("");
        student.studentId = null;
        
        context.Add(student);
        await context.SaveChangesAsync();

        var tutor = TestEntityGenerator.aTutor(student.studentId!);
        context.Add(tutor);
        await context.SaveChangesAsync();
        
        student.tutor = tutor;
    }

    private async Task seedTariff(DbContext context, string schoolyearId)
    {
        var tariffType = new TariffTypeEntity
        {
            typeId = 4,
            description = "Matricula"
        };

        context.Add(tariffType);
        await context.SaveChangesAsync();
        
        var tariff = TestEntityGenerator.aTariff();
        tariff.type = 4;
        tariff.schoolYear = schoolyearId;

        context.Add(tariff);
        await context.SaveChangesAsync();
    }
}