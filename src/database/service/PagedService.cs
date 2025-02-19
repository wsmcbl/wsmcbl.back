using Microsoft.EntityFrameworkCore;
using wsmcbl.src.model;

namespace wsmcbl.src.database.service;

public class PagedService<T> where T : class
{
    private IQueryable<T> query { get; set; }
    private Func<IQueryable<T>, string, IQueryable<T>> search { get; set; }
    
    public PagedService(IQueryable<T> query, Func<IQueryable<T>, string, IQueryable<T>> search)
    {
        this.query = query; 
        this.search = search;
    }
    
    public async Task<PagedResult<T>> getPaged(PagedRequest request)
    {
        if (!string.IsNullOrWhiteSpace(request.search))
        {
            query = search(query, request.search);
        }

        if (!string.IsNullOrWhiteSpace(request.sortBy))
        {
            query = request.isAscending
                ? query.OrderBy(e => EF.Property<object>(e, request.sortBy!))
                : query.OrderByDescending(e => EF.Property<object>(e, request.sortBy!));
        }

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
}