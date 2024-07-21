using Newtonsoft.Json;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.input;

public class SchoolYearToCreateDto
{
    [JsonRequired] public List<GradeDto> grades { get; set; } = null!;
    [JsonRequired] public List<TariffDto> tariffs { get; set; } = null!;

    public List<GradeEntity> getGradeList()
    {
        return grades.Select(e => e.toEntity()).ToList();
    }

    public List<TariffEntity> getTariffList()
    {
        return tariffs.Select(e => e.toEntity()).ToList();
    }
}