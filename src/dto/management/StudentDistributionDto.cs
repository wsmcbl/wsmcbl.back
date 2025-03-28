using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.management;

public class DistributionStudentDto
{
    public int total { get; set; }
    public int males { get; set; }
    public int droppedOut { get; set; }
    public List<DistributionByLevelDto> levelList { get; set; } = null!;
    public List<DistributionByDegreeDto> degreeList { get; set; } = null!;

    public DistributionStudentDto(List<StudentRegisterView> studentList, List<DegreeEntity> degreeList)
    {
        total = studentList.Count;
        males = studentList.Count(e => e.sex);
        droppedOut = 0;

        setSummaryStudentByLevel(studentList);
        setSummaryStudentByDegree(studentList, degreeList);
    }

    private void setSummaryStudentByLevel(List<StudentRegisterView> studentList)
    {
        List<string> LevelList = [ "Preescolar", "Primaria", "Secundaria"];

        levelList = [];
        foreach (var level in LevelList)
        {
            var list = studentList.Where(e => e.educationalLevel == level).ToList();
            addLevel(getLevel(level), list.Count,list.Count(e => e.sex));
        }
    }

    private void setSummaryStudentByDegree(List<StudentRegisterView> studentList, List<DegreeEntity> DegreeList)
    {
        degreeList = [];
        foreach (var item in DegreeList)
        {
            var list = studentList.Where(e => e.degree == item.label).ToList();
            addDegree(item.label,item.tag, item.educationalLevel, list.Count,list.Count(e => e.sex));
        }
    }

    private void addLevel(int level, int count, int man)
    {
        levelList.Add(new DistributionByLevelDto(level, count, man));
    }

    private void addDegree(string label, string position, string educationalLevel, int count, int man)
    { 
        degreeList.Add(new DistributionByDegreeDto(label,Convert.ToInt32(position), getLevel(educationalLevel),
            count, man));
    }

    private static int getLevel(string educationalLevel)
    {
        return educationalLevel switch
        {
            "Preescolar" => 1,
            "Primaria" => 2,
            "Secundaria" => 3,
            _ => 0
        };
    }
}