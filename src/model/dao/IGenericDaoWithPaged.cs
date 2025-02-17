namespace wsmcbl.src.model.dao;

public interface IGenericDaoWithPaged<T, in ID> : IGenericDao<T, ID>
{
    public Task<PagedResult<T>> getPaged(PagedRequest request);
    public IQueryable<P> search<P>(IQueryable<P> query, PagedRequest request);
    public IQueryable<P> filter<P>(IQueryable<P> query, PagedRequest request);
    public IQueryable<P> sort<P>(IQueryable<P> query, PagedRequest request);
}