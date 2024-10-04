using Microsoft.EntityFrameworkCore;
using wsmcbl.src.model.accounting;
using wsmcbl.tests.utilities;

namespace wsmcbl.tests.integration;

public class PrintReportCardByStudentFixture : BaseFixture
{
    protected override async Task seedData(DbContext context)
    {
        var schoolyear = TestEntityGenerator.aSchoolYear();
        schoolyear.id = null;
        context.Add(schoolyear);
        await context.SaveChangesAsync();
        
        var tariffType = new TariffTypeEntity
        {
            typeId = 4,
            description = "Matricula"
        };

        context.Add(tariffType);
        await context.SaveChangesAsync();
        
        var tariff = TestEntityGenerator.aTariff();
        tariff.type = 4;
        tariff.schoolYear = schoolyear.id;

        context.Add(tariff);
        await context.SaveChangesAsync();
        
        var discount = new DiscountEntity()
        {
            discountId = 1,
            amount = 0,
            description = "Nada"
        };

        context.Add(discount);
        await context.SaveChangesAsync();
        
        var student = TestEntityGenerator.aStudent("");
        student.studentId = null;
        
        context.Add(student);
        await context.SaveChangesAsync();
    }
}