using wsmcbl.src.model.academy;
using wsmcbl.src.model.accounting;

namespace wsmcbl.src.model.secretary;

public class SchoolyearEntity
{
    public string? id { get; set; }
    public string label { get; set; } = null!;
    public DateOnly startDate { get; set; }
    public DateOnly deadLine { get; set; }
    public bool isActive { get; set; }
    
    public List<DegreeEntity>? degreeList { get; set; }
    public List<TariffEntity>? tariffList { get; set; }
    public ExchangeRateEntity? exchangeRate { get; set; }
    public List<SemesterEntity>? semesterList { get; set; }
    
    public void setDegreeDataList(List<DegreeDataEntity> list)
    {
        degreeList = list.Select(e => new DegreeEntity(e, id!)).ToList();
    }

    public void setTariffList(List<TariffEntity> tariffEntities)
    {
        tariffList = [];
        foreach (var item in tariffEntities)
        {
            item.schoolyearId = id!;
            tariffList.Add(item);
        }
    }

    public async Task createExchangeRate(IExchangeRateDao exchangeRateDao)
    {
        exchangeRate = new ExchangeRateEntity
        {
            schoolyearId = id!,
            value = 0
        };
        
        exchangeRateDao.create(exchangeRate);
        await exchangeRateDao.saveAsync();
    }
}