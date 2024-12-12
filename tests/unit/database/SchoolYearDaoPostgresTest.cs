using wsmcbl.src.database;
using wsmcbl.src.model.secretary;
using wsmcbl.tests.utilities;

namespace wsmcbl.tests.unit.database;

public class SchoolYearDaoPostgresTest
{
    [Fact]
    public async Task getNewSchoolYear_ShouldReturnNextSchoolyear_WhenItsMiddleYear()
    {
        var schoolyear = TestEntityGenerator.aSchoolYear();
        schoolyear.label = (DateTime.Now.Year + 1).ToString();
        var context = TestDbContext.getInMemory();
        
        context.Set<SchoolYearEntity>().Add(schoolyear);
        await context.SaveChangesAsync();
        
        var sut = new SchoolyearDaoPostgres(context);

        var result = await sut.getOrCreateNewSchoolyear();

        Assert.NotNull(result);
        Assert.Equal(schoolyear, result);
    }
    
    [Fact]
    public async Task getNewSchoolYear_ShouldCreateAndReturnSchoolyear_WhenCalled()
    {
        var context = TestDbContext.getInMemory();
        
        var sut = new SchoolyearDaoPostgres(context);

        var result = await sut.getOrCreateNewSchoolyear();

        Assert.NotNull(result);
        Assert.IsType<SchoolYearEntity>(result);

        Assert.True(result.isActive);
        Assert.NotNull(result.label);
        Assert.Equal(result.label, result.startDate.Year.ToString());
        Assert.Equal(result.label, result.deadLine.Year.ToString());
    }
}