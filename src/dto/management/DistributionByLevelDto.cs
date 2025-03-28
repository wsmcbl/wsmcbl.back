namespace wsmcbl.src.dto.management;

public class DistributionByLevelDto
{
    public int educationalLevel { get; set; }
    public int total { get; set; }
    public int males { get; set; }

    public DistributionByLevelDto(int educationalLevel, int total, int males)
    {
        this.educationalLevel = educationalLevel;
        this.total = total;
        this.males = males;
    }
}