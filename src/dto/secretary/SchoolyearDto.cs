using wsmcbl.src.dto.management;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.secretary;

public class SchoolyearDto
{
    public string schoolyearId { get; set; }
    public string label { get; set; }
    public DateOnly startDate { get; set; }
    public DateOnly deadLine { get; set; }
    public bool isActive { get; set; }
    

    public ExchangeRateEntity? exchangeRate { get; set; }

    public List<PartialDto>? partialList { get; set; }
    
    public List<DegreeSubjectDto>? degreeList { get; set; }
    
    public List<TariffToCreateDto>? tariffList { get; set; }

    public SchoolyearDto(SchoolYearEntity schoolyear)
    {
        schoolyearId = schoolyear.id!;
        label = schoolyear.label;
        isActive = schoolyear.isActive;
        startDate = schoolyear.startDate;
        deadLine = schoolyear.deadLine;

        exchangeRate = schoolyear.exchangeRate;

        setPartialList(schoolyear.partialList);
        setDegreeList(schoolyear.degreeList);
        setTariffList(schoolyear.tariffList);
    }

    private void setPartialList(List<PartialEntity>? list)
    {
        if (list == null || list.Count == 0)
        {
            partialList = [];
            return;
        }

        partialList = list.mapListToDto();
    }

    private void setDegreeList(List<DegreeEntity>? list)
    {
        if (list == null || list.Count == 0)
        {
            degreeList = [];
            return;
        }

        degreeList = list.mapListToDto();
    }

    private void setTariffList(List<TariffEntity>? list)
    {
        if (list == null || list.Count == 0)
        {
            tariffList = [];
            return;
        }

        tariffList = list.mapListToDto();
    }
}