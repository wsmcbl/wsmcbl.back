using Newtonsoft.Json;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.secretary;

public class SchoolYearToCreateDto
{
    [JsonRequired] public List<DegreeToCreateDto> degreeList { get; set; } = null!;
    [JsonRequired] public List<TariffToCreateDto> tariffList { get; set; } = null!;
    [JsonRequired] public List<PartialToCreateDto> partialList { get; set; } = null!;

    public List<DegreeEntity> getGradeList()
    {
        return degreeList.Select(e => e.toEntity()).ToList();
    }

    public List<TariffEntity> getTariffList()
    {
        return tariffList.Select(e => e.toEntity()).ToList();
    }

    public List<PartialEntity> getPartialList()
    {
        return partialList.Select(e => e.toEntity()).ToList();
    }
}