namespace wsmcbl.src.model.dao;

public class PagedRequest
{
    public string? search { get; set; }
    public string? sortBy { get; set; }
    public int page { get; set; } = 1;
    public int pageSize { get; set; } = 10;
    public bool isAscending { get; set; } = true;
}