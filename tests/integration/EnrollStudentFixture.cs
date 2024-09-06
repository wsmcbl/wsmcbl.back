using wsmcbl.src.database.context;
using wsmcbl.tests.utilities;

namespace wsmcbl.tests.integration;

public class EnrollStudentFixture : BaseClassFixture
{
    protected override void seedData(PostgresContext dbContext)
    {
        var schoolyear = TestEntityGenerator.aSchoolYear();
        schoolyear.id = "id";
        
        dbContext.Add(schoolyear);
        dbContext.SaveChangesAsync();
    }
}