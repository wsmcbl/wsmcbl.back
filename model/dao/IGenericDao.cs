namespace wsmcbl.back.model.dao;

public interface IGenericDao<T, ID>
{
    public Task create(T entity);
    public Task<T?> getById(ID id);
    public void update(T entity);
    public void deleteById(ID id);
    public Task<List<T>> getAll();
}