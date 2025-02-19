namespace wsmcbl.src.model.dao;

public interface IGenericDaoWithPaged<T, in ID, P> : IGenericDao<T, ID> 
{
    public Task<PagedResult<P>> getPaged(PagedRequest request);
    public IQueryable<P> search(IQueryable<P> query, PagedRequest request);
    public IQueryable<P> sort(IQueryable<P> query, PagedRequest request);
}