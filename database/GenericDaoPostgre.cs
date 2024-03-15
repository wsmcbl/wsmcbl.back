using Microsoft.EntityFrameworkCore;
using wsmcbl.back.model.dao;

namespace wsmcbl.back.database;

public abstract class GenericDaoPostgre<T, ID> : IGenericDao<T, ID> where T : class
{
    protected readonly PostgresContext context;
    protected GenericDaoPostgre(PostgresContext context)
    {
        this.context = context;
    }
    

    public void create(T entity)
    {
        throw new NotImplementedException();
    }

    public T read(ID id)
    {
        throw new NotImplementedException();
    }

    public void update(T entity)
    {
        throw new NotImplementedException();
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