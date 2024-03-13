using Microsoft.EntityFrameworkCore;
using wsmcbl.back.model.dao;

namespace wsmcbl.back.database;

public abstract class GenericDaoPostgre<T, ID> : IGenericDao<T, ID>
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

    public List<T> getAll()
    {
        throw new NotImplementedException();
    }
}