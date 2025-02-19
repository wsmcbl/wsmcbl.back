using Microsoft.EntityFrameworkCore;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.database.service;

public class PagedService<T> where T : class
{
    private IQueryable<T> query { get; set; }
    
    public PagedService(IQueryable<T> query)
    {
        this.query = query; 
    }
    
    public async Task<PagedResult<T>> getPaged(PagedRequest request)
    {
        if (!string.IsNullOrWhiteSpace(request.search))
        {
            query = search(request);
        }

        query = sort(request);

        var totalCount = await query.CountAsync();

        var data = await query
            .Skip((request.page - 1) * request.pageSize)
            .Take(request.pageSize)
            .ToListAsync();

        return new PagedResult<T>
        {
            data = data,
            quantity = totalCount,
            page = request.page,
            pageSize = request.pageSize
        };
    }

    public IQueryable<T> search(PagedRequest request)
    {
        return query;
    }

    public IQueryable<T> sort(PagedRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.sortBy))
        {
            return query;
        }

        return request.isAscending
            ? query.OrderBy(e => EF.Property<object>(e, request.sortBy!))
            : query.OrderByDescending(e => EF.Property<object>(e, request.sortBy!));
    }
}