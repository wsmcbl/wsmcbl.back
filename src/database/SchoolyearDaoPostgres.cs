using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.database;

public class SchoolyearDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<SchoolyearEntity, string>(context), ISchoolyearDao
{
    public async Task<SchoolyearEntity> getByLabel(int year)
    {
        var result = await entities.FirstOrDefaultAsync(e => e.label == year.ToString());
        if (result == null)
        {
            throw new EntityNotFoundException($"Entity of type (SchoolyearEntity) with label ({year}) not found.");
        }

        return result;
    }
    
    public async Task<SchoolyearEntity> getCurrent(bool withProperties = true)
    {
        var year = DateTime.Today.Year.ToString();
        var query = entities.Where(e => e.label == year);
        
        if (withProperties)
        {
            query = query.Include(e => e.exchangeRate)
                .Include(e => e.semesterList)
                .Include(e => e.degreeList)
                .Include(e => e.tariffList);
        }

        var result = await query.FirstOrDefaultAsync();
        if (result == null)
        {
            throw new EntityNotFoundException($"Entity of type (SchoolyearEntity) with label ({year}) not found.");
        }
        
        return result;
    }
    
    private static int newOrCurrentLabel() => DateTime.Today.Month > 10 ? DateTime.Today.Year + 1 : DateTime.Today.Year;
    
    public async Task<SchoolyearEntity> getCurrentOrNew()
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

    public async Task<SchoolyearEntity> getNewOrCurrent()
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

    public async Task<SchoolyearEntity> getOrCreateNew()
    {
        try
        {
            return await getByLabel(newOrCurrentLabel());
        }
        catch (EntityNotFoundException)
        {
            var year = newOrCurrentLabel();
            
            var schoolYearEntity = new SchoolyearEntity
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