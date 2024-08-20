using wsmcbl.src.model.accounting;

namespace wsmcbl.src.model.secretary;

public class SchoolYearEntity
{
    public string? id { get; set; }
    public string label { get; set; } = null!;
    public DateOnly startDate { get; set; }
    public DateOnly deadLine { get; set; }
    public bool isActive { get; set; }
    
    public List<DegreeEntity>? degreeList { get; private set; }
    public List<TariffEntity>? tariffList { get; private set; }

    public void setGradeList(List<DegreeEntity> list)
    {
        degreeList = list;
    }
    
    public void setTariffList(List<TariffEntity> list)
    {
        tariffList = list;
    }
    
    public void setGradeDataList(List<DegreeDataEntity> list)
    {
        degreeList = [];
        foreach (var item in list)
        {
            degreeList.Add(new DegreeEntity(item, id!));
        }
    }

    public void setTariffDataList(List<TariffDataEntity> list)
    {
        tariffList = [];
        foreach (var item in list)
        {
            if (item.dueDate != null)
            {
                item.dueDate = new DateOnly(int.Parse(label), item.dueDate.Value.Month, item.dueDate.Value.Day);   
            }
            
            tariffList.Add(new TariffEntity(item, id!));
        }
    }
}