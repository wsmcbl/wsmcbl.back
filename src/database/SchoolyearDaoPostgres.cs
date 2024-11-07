using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.database;

public class SchoolyearDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<SchoolYearEntity, string>(context), ISchoolyearDao
{
    public async Task<SchoolYearEntity> getCurrentSchoolyear()
    {
        var result = await getSchoolYearByLabel(DateTime.Today.Year);
        
        var gradeList = await context.Set<DegreeEntity>().Where(e => e.schoolYear == result.id).ToListAsync();
        result.setGradeList(gradeList);

        var tariffList = await context.Set<TariffEntity>().Where(e => e.schoolYear == result.id).ToListAsync();
        result.setTariffList(tariffList);
        
        return result;
    }

    public async Task<SchoolYearEntity?> getNewSchoolyear()
    {
        try
        {
            return await getSchoolYearByLabel(getLabelOfTheNewSchoolYear());
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<SchoolYearEntity> getOrCreateNewSchoolyear()
    {
        try
        {
            var result = await getNewSchoolyear();

            if (result == null)
            {
                throw new ConflictException("new Schoolyear not found");
            }

            return result;
        }
        catch (Exception)
        {
            var year = getLabelOfTheNewSchoolYear();
            
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
            throw new EntityNotFoundException($"Entity of type (Schoolyear) with label ({year}) not found.");
        }

        return result;
    }

    private static int getLabelOfTheNewSchoolYear() => DateTime.Today.Month > 10 ? DateTime.Today.Year + 1 : DateTime.Today.Year;
}