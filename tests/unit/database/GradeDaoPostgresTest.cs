using wsmcbl.src.database;
using wsmcbl.src.exception;
using wsmcbl.src.model.secretary;
using wsmcbl.tests.utilities;

namespace wsmcbl.tests.unit.database;

public class GradeDaoPostgresTest
{
    [Fact]
    public async Task getById_ShouldThrowException_WhenGradeNotExist()
    {
        var context = TestDbContext.getInMemory();
        var sut = new GradeDaoPostgres(context);

        await Assert.ThrowsAsync<EntityNotFoundException>(() => sut.getById("gr-0001"));
    }
    
    [Fact]
    public async Task getById_ShouldReturnGrade_WhenIdIsProvide()
    {
        var grade = TestEntityGenerator.aGrade("gr-0001");
        var context = TestDbContext.getInMemory();
        context.Set<GradeEntity>().Add(grade);
        await context.SaveChangesAsync();
        
        var sut = new GradeDaoPostgres(context);

        var result = await sut.getById("gr-0001");

        Assert.NotNull(result);
        Assert.Equivalent(grade, result);
    }
}