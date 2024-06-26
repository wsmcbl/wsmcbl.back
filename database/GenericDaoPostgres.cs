using Microsoft.EntityFrameworkCore;
using wsmcbl.back.model.dao;

namespace wsmcbl.back.database;

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

    public void deleteById(ID id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<T>> getAll()
    {
        var elements = await context.Set<T>().ToListAsync();
        return elements;
    }
}