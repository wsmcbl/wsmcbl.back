namespace wsmcbl.src.model.dao;

public class PagedResult<T>
{
    public List<T> data { get; set; } = [];
    public int page { get; set; }
    public int pageSize { get; set; }
    public int quantity { get; set; }
    public int totalPages => (int)Math.Ceiling((double)quantity / pageSize);

    public PagedResult()
    {
    }

    public PagedResult(List<T> list)
    {
        data = list;
    }

    public void setup<P>(PagedResult<P> parameter) where P : class
    {
        page = parameter.page;
        pageSize = parameter.pageSize;
        quantity = parameter.quantity;
    }
}