namespace wsmcbl.src.dto.management;

public class SummaryStudentDto
{
    public int total { get; set; }
    public int males { get; set; }
    public int droppedOut { get; set; }
    public List<SummaryByLevelDto> levelList { get; set; }
    public List<SummaryByDegreeDto> degreeList { get; set; }

    public SummaryStudentDto(int total, int males, int withdrawn)
    {
        this.total = total;
        this.males = males;
        droppedOut = withdrawn;
        levelList = [];
        degreeList = [];
    }

    public void addLevel(int level, int count, int man)
    {
        levelList.Add(new SummaryByLevelDto(level, count, man));
    }

    public void addDegree(string label, string position, int level, int count, int man)
    {
        degreeList.Add(new SummaryByDegreeDto(label, Convert.ToInt32(position), level, count, man));
    }
}