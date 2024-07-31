using wsmcbl.src.database;
using wsmcbl.src.exception;
using wsmcbl.src.model.academy;
using wsmcbl.tests.utilities;

namespace wsmcbl.tests.unit.database;

public class EnrollmentDaoPostgresTest
{
    [Fact]
    public async Task getAll_ShouldReturnList_WhenCalled()
    {
        var enrollment = TestEntityGenerator.aEnrollment();
        var context = TestDbContext.getInMemory();
        context.Set<EnrollmentEntity>().Add(enrollment);
        await context.SaveChangesAsync();
        
        var sut = new EnrollmentDaoPostgres(context);

        var result = await sut.getAll();

        Assert.NotNull(result);
        Assert.Equivalent(new List<EnrollmentEntity>{enrollment}, result); 
    }
    
    
    [Fact]
    public async Task getById_ShouldReturnEnrollment_WhenCalled()
    {
        var enrollment = TestEntityGenerator.aEnrollment();
        var context = TestDbContext.getInMemory();
        context.Set<EnrollmentEntity>().Add(enrollment);
        await context.SaveChangesAsync();
        
        var sut = new EnrollmentDaoPostgres(context);

        var result = await sut.getById(enrollment.enrollmentId!);

        Assert.NotNull(result);
        Assert.Equal(enrollment, result);
    }
}