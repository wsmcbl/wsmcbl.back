namespace wsmcbl.src.model.dao;

public interface IGenericDao<T, in ID>
{
    public Task create(T entity);
    public Task<T?> getById(ID id);
    public Task update(T entity);
    public Task<List<T>> getAll();
}