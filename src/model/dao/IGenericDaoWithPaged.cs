namespace wsmcbl.src.model.dao;

public interface IGenericDaoWithPaged<T, in ID> : IGenericDao<T, ID>
{
    public Task<PagedResult<T>> getPaged(PagedRequest request);
    public IQueryable<T> search(IQueryable<T> query, PagedRequest request);
    public IQueryable<T> filter(IQueryable<T> query, PagedRequest request);
}