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
        return new SummaryStudentDto(total, males, 0);
    }

    private async Task setSummaryStudentByArea(SummaryStudentDto dto)
    {
        var list = studentList.Where(e => e.educationalLevel!.Equals("Preescolar")).ToList();
        var area1 = new SummaryByAreaDto(list.Count, list.Count(e => e.sex));
        dto.areaList.Add(area1);
        
        list = studentList.Where(e => e.educationalLevel!.Equals("Primaria")).ToList();
        var area2 = new SummaryByAreaDto(list.Count, list.Count(e => e.sex));
        dto.areaList.Add(area2);
        
        list = studentList.Where(e => e.educationalLevel!.Equals("Secundaria")).ToList();
        var area3 = new SummaryByAreaDto(list.Count, list.Count(e => e.sex));
        dto.areaList.Add(area3);
    }

    private async Task setSummaryStudentByDegree(SummaryStudentDto dto)
    {
        var schoolyear = await daoFactory.schoolyearDao!.getCurrentOrNew();
        var degreeList = await daoFactory.degreeDao!.getListForSchoolyearId(schoolyear.id!);

        degreeList = degreeList.OrderBy(e => e.educationalLevel).ThenBy(e => e.tag).ToList();
    }
    

    public async Task<object?> getSummaryTeacherGrades()
    {
        await Task.CompletedTask;
        return 0;
    }
}