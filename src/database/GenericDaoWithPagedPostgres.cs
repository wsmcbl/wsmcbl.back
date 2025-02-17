using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.database;

public class GenericDaoWithPagedPostgres<T, ID> : GenericDaoPostgres<T, ID>, IGenericDaoWithPaged<T, ID> where T : class
{
    public GenericDaoWithPagedPostgres(PostgresContext context) : base(context)
    {
    }

    public async Task<PagedResult<T>> getPaged(PagedRequest request)
    {
        var query = context.Set<T>().AsQueryable();
        return await getPaged(query, request);
    }

    protected async Task<PagedResult<P>> getPaged<P>(IQueryable<P> query, PagedRequest request) where P : class
    { 
        var totalCount = await query.CountAsync();

        query = search(query, request);
        query = filter(query, request);
        query = sort(query, request);
        
        if (!string.IsNullOrEmpty(request.sortBy))
        {
            query = !request.isDescending ? query.OrderBy(e => EF.Property<object>(e, request.sortBy))
                : query.OrderByDescending(e => EF.Property<object>(e, request.sortBy));
        }

        var data = await query
            .Skip((request.page - 1) * request.pageSize)
            .Take(request.pageSize)
            .ToListAsync();

        return new PagedResult<P>
        {
            data = data,
            quantity = totalCount,
            page = request.page,
            pageSize = request.pageSize
        };
    }
    
    public IQueryable<P> search<P>(IQueryable<P> query, PagedRequest request)
    {
        throw new NotImplementedException();
    }

    public IQueryable<P> filter<P>(IQueryable<P> query, PagedRequest request)
    {
        throw new NotImplementedException();
    }

    public IQueryable<P> sort<P>(IQueryable<P> query, PagedRequest request)
    {
        throw new NotImplementedException();
    }
}