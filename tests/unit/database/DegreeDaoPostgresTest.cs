using wsmcbl.src.database;
using wsmcbl.src.exception;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.secretary;
using wsmcbl.tests.utilities;

namespace wsmcbl.tests.unit.database;

public class DegreeDaoPostgresTest : BaseDaoPostgresTest
{
    [Fact]
    public async Task createRange_ShouldCreateList_WhenCalled()
    {
        var grade = TestEntityGenerator.aDegree("gd-001");
        context = TestDbContext.getInMemory();
        
        var sut = new DegreeDaoPostgres(context);
        
        await sut.createRange([grade]);
        
        await context.SaveChangesAsync();
        Assert.Equal(grade, context.Set<DegreeEntity>().First(e => e.degreeId == "gd-001"));
    }
    
    [Fact]
    public async Task getById_ShouldThrowException_WhenGradeNotExist()
    {
        context = TestDbContext.getInMemory();
        var sut = new DegreeDaoPostgres(context);

        await Assert.ThrowsAsync<EntityNotFoundException>(() => sut.getById("gr-0001"));
    }
    
    [Fact]
    public async Task getById_ShouldReturnGrade_WhenIdIsProvide()
    {
        var grade = TestEntityGenerator.aDegree("gr-0001");
        context = TestDbContext.getInMemory();
        context.Set<DegreeEntity>().Add(grade);
        await context.SaveChangesAsync();
        
        var sut = new DegreeDaoPostgres(context);

        var result = await sut.getById("gr-0001");

        Assert.NotNull(result);
        Assert.Equivalent(grade, result);
    }

    [Fact]
    public async Task getValidListForTheSchoolyear_ShouldReturnList_WhenSchoolyearExist()
    {
        context = TestDbContext.getInMemory();
        
        var schoolyear = TestEntityGenerator.aSchoolYear();
        context.Set<SchoolYearEntity>().Add(schoolyear);

        var degree = TestEntityGenerator.aDegree("dgr001");
        degree.schoolYear = schoolyear.id!;
        context.Set<DegreeEntity>().Add(degree);
        await context.SaveChangesAsync();
        
        var enrollment = TestEntityGenerator.aEnrollment();
        enrollment.degreeId = degree.degreeId!;
        context.Set<EnrollmentEntity>().Add(enrollment);
        await context.SaveChangesAsync();
        
        var sut = new DegreeDaoPostgres(context);

        var result = await sut.getValidListForTheSchoolyear();
        
        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task getValidListForTheSchoolyear_ShouldReturnEmptyList_WhenSchoolyearNotExist()
    {
        var sut = new DegreeDaoPostgres(TestDbContext.getInMemory());

        var result = await sut.getValidListForTheSchoolyear();
        Assert.Empty(result);
    }
}