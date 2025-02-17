namespace wsmcbl.src.controller.service;

public class PagedResult<T>
{
    public List<T> data { get; set; } = [];
    public int page { get; set; }
    public int pageSize { get; set; }
    public int quantity { get; set; }
    public int totalPages => (int)Math.Ceiling((double)quantity / pageSize);    
}