using wsmcbl.src.database;
using wsmcbl.src.exception;
using wsmcbl.src.model.secretary;
using wsmcbl.tests.utilities;

namespace wsmcbl.tests.unit.database;

public class DegreeDaoPostgresTest
{
    [Fact]
    public async Task crateList_ShouldCreateList_WhenCalled()
    {
        var grade = TestEntityGenerator.aGrade("gd-001");
        var context = TestDbContext.getInMemory();
        
        var sut = new DegreeDaoPostgres(context);
        
        sut.createList([grade]);
        
        await context.SaveChangesAsync();
        Assert.Equal(grade, context.Set<DegreeEntity>().First(e => e.degreeId == "gd-001"));
    }
    
    [Fact]
    public async Task getById_ShouldThrowException_WhenGradeNotExist()
    {
        var context = TestDbContext.getInMemory();
        var sut = new DegreeDaoPostgres(context);

        await Assert.ThrowsAsync<EntityNotFoundException>(() => sut.getById("gr-0001"));
    }
    
    [Fact]
    public async Task getById_ShouldReturnGrade_WhenIdIsProvide()
    {
        var grade = TestEntityGenerator.aGrade("gr-0001");
        var context = TestDbContext.getInMemory();
        context.Set<DegreeEntity>().Add(grade);
        await context.SaveChangesAsync();
        
        var sut = new DegreeDaoPostgres(context);

        var result = await sut.getById("gr-0001");

        Assert.NotNull(result);
        Assert.Equivalent(grade, result);
    }
}