using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.management;

public class SummaryStudentDto
{
    public int total { get; set; }
    public int males { get; set; }
    public int droppedOut { get; set; }
    public List<DistributionByLevelDto> levelList { get; set; }
    public List<SummaryByDegreeDto> degreeList { get; set; }

    public SummaryStudentDto(List<StudentRegisterView> studentList, List<DegreeEntity> degreeList)
    {
        this.total = total;
        this.males = males;
        droppedOut = 0;
        levelList = [];
        this.degreeList = [];
    }

    public void addLevel(int level, int count, int man)
    {
        levelList.Add(new DistributionByLevelDto(level, count, man));
    }

    public void addDegree(string label, string position, int level, int count, int man)
    {
        degreeList.Add(new SummaryByDegreeDto(label, Convert.ToInt32(position), level, count, man));
    }
}