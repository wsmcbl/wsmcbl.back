using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.database;

public class SchoolyearDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<SchoolYearEntity, string>(context), ISchoolyearDao
{
    public async Task<SchoolYearEntity> getByLabel(int year)
    {
        var result = await entities.FirstOrDefaultAsync(e => e.label == year.ToString());
        if (result == null)
        {
            throw new EntityNotFoundException($"Entity of type (Schoolyear) with label ({year}) not found.");
        }

        return result;
    }
    
    public async Task<SchoolYearEntity> getCurrent(bool withProperties = true)
    {
        var result = await getByLabel(DateTime.Today.Year);
        if (!withProperties)
        {
            return result;
        }
        
        var gradeList = await context.Set<DegreeEntity>()
            .Where(e => e.schoolYear == result.id).ToListAsync();
        result.setGradeList(gradeList);

        var tariffList = await context.Set<TariffEntity>()
            .Where(e => e.schoolYear == result.id).ToListAsync();
        result.setTariffList(tariffList);

        return result;
    }
    
    private static int newOrCurrentLabel() => DateTime.Today.Month > 10 ? DateTime.Today.Year + 1 : DateTime.Today.Year;
    
    public async Task<SchoolYearEntity> getCurrentOrNew()
    {
        try
        {
            return await getCurrent(false);
        }
        catch (EntityNotFoundException)
        {
            return await getByLabel(newOrCurrentLabel());
        }
        catch (Exception)
        {
            throw new EntityNotFoundException("There is not valid schoolyear.");
        }
    }

    public async Task<SchoolYearEntity> getNewOrCurrent()
    {
        try
        {
            return await getByLabel(newOrCurrentLabel());
        }
        catch (EntityNotFoundException)
        {
            return await getCurrent(false);
        }
        catch (Exception)
        {
            throw new EntityNotFoundException("There is not valid schoolyear.");
        }
    }

    public async Task<SchoolYearEntity> getOrCreateNew()
    {
        try
        {
            return await getByLabel(newOrCurrentLabel());
        }
        catch (EntityNotFoundException)
        {
            var year = newOrCurrentLabel();
            
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
}