namespace wsmcbl.src.model;

public class PagedRequest
{
    public string? search { get; set; }
    public string? sortBy { get; set; }
    public int page { get; set; } = 1;
    public int pageSize { get; set; } = 10;
    public bool isAscending { get; set; } = true;

    public void setDefaultSort(string parameter)
    {
        sortBy ??= parameter;
    }
}