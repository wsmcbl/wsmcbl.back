namespace wsmcbl.src.dto.management;

public class DistributionByDegreeDto
{
    public string label { get; set; }
    public int educationalLevel { get; set; }
    public int position { get; set; }
    public int total { get; set; }
    public int males { get; set; }

    public DistributionByDegreeDto(string label, int position, int educationalLevel, int total, int males)
    {
        this.educationalLevel = educationalLevel;
        this.label = label;
        this.position = position;
        this.total = total;
        this.males = males;
    }
}