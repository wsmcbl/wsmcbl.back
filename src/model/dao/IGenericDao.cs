namespace wsmcbl.src.model.dao;

public interface IGenericDao<T, in ID>
{
    public void create(T entity);
    public Task<T?> getById(ID id);
    public void update(T entity);
    public Task<List<T>> getAll();
    public Task delete(T entity);
    internal Task saveAsync();
    public Task<PagedResult<T>> getPaged(PagedQuery request);
}