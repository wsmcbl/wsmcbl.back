using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.database;

public abstract class GenericDaoPostgres<T, ID> : IGenericDao<T, ID> where T : class
{
    protected readonly DbSet<T> entities;
    protected PostgresContext context { get; }

    protected GenericDaoPostgres(PostgresContext context)
    {
        entities = context.Set<T>();
        this.context = context;
    }

    public virtual void create(T entity)
    {
        entities.Add(entity);
    }

    public virtual async Task<T?> getById(ID id)
    {
        return await entities.FindAsync(id);
    }

    public void update(T entity)
    {
        entities.Update(entity);
    }

    public async Task<List<T>> getAll()
    {
        return await entities.ToListAsync();
    }

    public async Task delete(T entity)
    {
        entities.Remove(entity);
        await context.SaveChangesAsync();
    }

    public async Task saveAsync()
    {
        try
        {
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new ForbiddenException("Failed to perform transaction. Error: " + e.Message);
        }
    }
}