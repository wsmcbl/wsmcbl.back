namespace wsmcbl.src.dto.management;

public class SummaryByLevelDto
{
    public int position { get; set; }
    public int total { get; set; }
    public int males { get; set; }

    public SummaryByLevelDto(int position, int total, int males)
    {
        this.position = position;
        this.total = total;
        this.males = males;
    }
}