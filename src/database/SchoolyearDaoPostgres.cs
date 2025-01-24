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
    
    public async Task<SchoolYearEntity> getCurrentSchoolyear(bool withProperties = true)
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

    public async Task<SchoolYearEntity> getCurrentOrNewSchoolyear()
    {
        try
        {
            return await getCurrentSchoolyear();
        }
        catch (Exception)
        {
            var newSchoolyear = await getNewSchoolyear();
            if (newSchoolyear == null)
            {
                throw new EntityNotFoundException("There is not valid schoolyear.");
            }
            
            return newSchoolyear;
        }
    }

    public async Task<SchoolYearEntity> getNewOrCurrentSchoolyear()
    {
        try
        {
            var newSchoolyear = await getNewSchoolyear();
            if (newSchoolyear == null)
            {
                throw new EntityNotFoundException("There is not valid schoolyear.");
            }
            
            return newSchoolyear;
        }
        catch (Exception)
        {
            return await getCurrentSchoolyear();
        }
    }

    public async Task<SchoolYearEntity> getOrCreateNewSchoolyear()
    {
        try
        {
            var result = await getNewSchoolyear();

            if (result == null)
            {
                throw new ConflictException("New Schoolyear not found");
            }

            return result;
        }
        catch (Exception)
        {
            var year = currentOrNewSchoolyearLabel();
            
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
    
    public async Task<(string currentSchoolyear, string newSchoolyear)> getCurrentAndNewSchoolyearIds()
    {
        string currentSchoolyearId;
        try
        {
            var currentSchoolyear = await getCurrentSchoolyear();
            currentSchoolyearId = currentSchoolyear.id!;
        }
        catch (Exception)
        {
            currentSchoolyearId = string.Empty;
        }

        var newSchoolyear = await getNewSchoolyear();
        var newSchoolyearId = newSchoolyear == null ? string.Empty : newSchoolyear.id!;

        return (currentSchoolyearId, newSchoolyearId);
    }
    
    
    private static int currentOrNewSchoolyearLabel()
        => DateTime.Today.Month > 10 ? DateTime.Today.Year + 1 : DateTime.Today.Year;
    
    private async Task<SchoolYearEntity?> getNewSchoolyear()
    {
        try
        {
            return await getByLabel(currentOrNewSchoolyearLabel());
        }
        catch (Exception)
        {
            return null;
        }
    }
}