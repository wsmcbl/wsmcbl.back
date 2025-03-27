namespace wsmcbl.src.dto.management;

public class SummaryStudentDto
{
    public int total { get; set; }
    public int males { get; set; }
    public int withdrawn { get; set; }
    public List<SummaryByLevelDto> areaList { get; set; }
    public List<SummaryByDegreeDto> degreeList { get; set; }

    public SummaryStudentDto(int total, int males, int withdrawn)
    {
        this.total = total;
        this.males = males;
        this.withdrawn = withdrawn;
        areaList = [];
        degreeList = [];
    }
}