using wsmcbl.src.dto.management;
using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.business;

public class ViewDirectorDashboardController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    private List<StudentRegisterView> studentList { get; set; } = null!;
    
    public async Task<SummaryStudentDto> getSummaryStudentQuantity()
    {
        studentList = await daoFactory.studentDao!.getStudentRegisterListForCurrentSchoolyear();
        
        var total = studentList.Count;
        var males = studentList.Count(e => e.sex);

        var result = new SummaryStudentDto(total, males, 0);
        setSummaryStudentByLevel(result);
        await setSummaryStudentByDegree(result);
        
        return result;
    }

    private void setSummaryStudentByLevel(SummaryStudentDto dto)
    {
        var levelList = new List<(int id, string label)>
        {
            (1, "Preescolar"),
            (2, "Primaria"),
            (3, "Secundaria")
        };

        foreach (var level in levelList)
        {
            var list = studentList.Where(e => e.educationalLevel == level.label).ToList();
            dto.addLevel(level.id,
                list.Count,
                list.Count(e => e.sex));
        }
    }

    private async Task setSummaryStudentByDegree(SummaryStudentDto dto)
    {
        var schoolyear = await daoFactory.schoolyearDao!.getCurrentOrNew();
        var degreeList = await daoFactory.degreeDao!.getListForSchoolyearId(schoolyear.id!);

        degreeList = degreeList.OrderBy(e => e.educationalLevel).ThenBy(e => e.tag).ToList();

        foreach (var item in degreeList)
        {
            var list = studentList.Where(e => e.degree == item.label).ToList();
            dto.addDegree(item.label,
                item.tag,
                getLevel(item.educationalLevel),
                list.Count,
                list.Count(e => e.sex));
        }
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

    public async Task<object?> getSummaryTeacherGrades()
    {
        await Task.CompletedTask;
        return 0;
    }
}