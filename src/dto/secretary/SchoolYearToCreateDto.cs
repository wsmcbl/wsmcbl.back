using Newtonsoft.Json;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.secretary;

public class SchoolYearToCreateDto
{
    [JsonRequired] public List<DegreeToCreateDto> degrees { get; set; } = null!;
    [JsonRequired] public List<TariffToCreateDto> tariffs { get; set; } = null!;

    public List<DegreeEntity> getGradeList()
    {
        return degrees.Select(e => e.toEntity()).ToList();
    }

    public List<TariffEntity> getTariffList()
    {
        return tariffs.Select(e => e.toEntity()).ToList();
    }
}