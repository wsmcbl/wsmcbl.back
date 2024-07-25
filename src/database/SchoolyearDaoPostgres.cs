using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.database;

public class SchoolyearDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<SchoolYearEntity, string>(context), ISchoolyearDao
{
    public async Task<SchoolYearEntity> getNewSchoolYear()
    {
        var schoolYearEntity = await getCurrentSchoolYear();

        if (schoolYearEntity != null)
        {
            return schoolYearEntity;
        }
        
        var year = getYear();
        schoolYearEntity = new SchoolYearEntity
        {
            label = year.ToString(),
            startDate = new DateOnly(year, 1, 1),
            deadLine = new DateOnly(year, 12, 31),
            isActive = true
        };
        
        create(schoolYearEntity);
        await context.SaveChangesAsync();

        return schoolYearEntity;
    }

    public async Task<SchoolYearEntity?> getCurrentSchoolYear()
    {
        return await entities.FirstOrDefaultAsync(e => e.label == getYear().ToString());
    }

    private static int getYear() => DateTime.Today.Month > 4 ? DateTime.Today.Year + 1 : DateTime.Today.Year;
}