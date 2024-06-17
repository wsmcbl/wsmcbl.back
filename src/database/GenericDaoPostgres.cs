using Microsoft.EntityFrameworkCore;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.database;

public abstract class GenericDaoPostgres<T, ID>(PostgresContext context) : IGenericDao<T, ID> where T : class
{
    protected readonly PostgresContext context = context;

    public virtual async Task create(T entity)
    {
        context.Set<T>().Add(entity);
        await context.SaveChangesAsync();
    }

    public virtual async Task<T?> getById(ID id)
    {
        var element = await context.Set<T>().FindAsync(id);
        return element;
    }

    public async Task update(T entity)
    {
        context.Set<T>().Update(entity);
        await context.SaveChangesAsync();
    }

    public async Task<List<T>> getAll()
    {
        var elements = await context.Set<T>().ToListAsync();
        return elements;
    }
}