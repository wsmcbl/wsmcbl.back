using wsmcbl.src.model.accounting;

namespace wsmcbl.src.model.secretary;

public class SchoolYearEntity
{
    public string id { get; set; } = null!;
    public string label { get; set; } = null!;
    public DateOnly startDate { get; set; }
    public DateOnly deadLine { get; set; }
    public bool isActive { get; set; }
    
    public List<GradeEntity>? gradeList { get; private set; }
    public List<TariffEntity>? tariffList { get; private set; }
    
    public void setGradeDataList(List<GradeDataEntity> list)
    {
        gradeList ??= [];
        foreach (var item in list)
        {
            gradeList.Add(new GradeEntity(item));
        }
    }

    public void setTariffDataList(List<TariffDataEntity> list)
    {
        tariffList ??= [];
        foreach (var item in list)
        {
            tariffList.Add(new TariffEntity(item, id));
        }
    }
}