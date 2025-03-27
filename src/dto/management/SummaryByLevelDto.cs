namespace wsmcbl.src.dto.management;

public class SummaryByLevelDto
{
    public int educationalLevel { get; set; }
    public int total { get; set; }
    public int males { get; set; }

    public SummaryByLevelDto(int educationalLevel, int total, int males)
    {
        this.educationalLevel = educationalLevel;
        this.total = total;
        this.males = males;
    }
}