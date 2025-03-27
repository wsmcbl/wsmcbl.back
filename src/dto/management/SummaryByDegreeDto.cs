namespace wsmcbl.src.dto.management;

public class SummaryByDegreeDto
{
    public int level { get; set; }
    public int position { get; set; }
    public int total { get; set; }
    public int males { get; set; }
    public string label { get; set; }

    public SummaryByDegreeDto(string label, int position, int level, int total, int males)
    {
        this.level = level;
        this.label = label;
        this.position = position;
        this.total = total;
        this.males = males;
    }
}