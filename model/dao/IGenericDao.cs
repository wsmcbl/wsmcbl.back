namespace wsmcbl.back.model.dao;

public interface IGenericDao<T, ID>
{
    public void create(T entity);
    public T read(ID id);
    public void update(T entity);
    public void deleteById(ID id);
    public Task<List<T>> getAll();
}