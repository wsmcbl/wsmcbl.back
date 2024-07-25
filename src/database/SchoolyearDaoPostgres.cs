using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.database;

public class SchoolyearDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<SchoolYearEntity, string>(context), ISchoolyearDao
{
    public async Task<SchoolYearEntity> getNewSchoolYear()
    {
        try
        {
            return await getSchoolYearByLabel(getNewSchoolyear());
        }
        catch (Exception e)
        {
            var year = getNewSchoolyear();
            
            var schoolYearEntity = new SchoolYearEntity
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
    }

    public async Task<SchoolYearEntity> getSchoolYearByLabel(int year)
    {
        var result = await entities.FirstOrDefaultAsync(e => e.label == year.ToString());

        if (result == null)
        {
            throw new EntityNotFoundException("Schoolyear", "");
        }

        return result;
    }

    private static int getNewSchoolyear() => DateTime.Today.Month > 4 ? DateTime.Today.Year + 1 : DateTime.Today.Year;
}