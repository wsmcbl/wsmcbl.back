using wsmcbl.src.model.academy;
using wsmcbl.src.model.accounting;

namespace wsmcbl.src.dto.secretary;

public class SchoolyearToCreateDto
{
    public List<PartialToCreateDto> partialList { get; set; } = null!;
    public List<TariffDto> tariffList { get; set; } = null!;
    
    public List<TariffEntity> getTariffList()
    {
        return tariffList.Select(e => e.toEntity()).ToList();
    }

    public List<PartialEntity> getPartialList()
    {
        return partialList.Select(e => e.toEntity()).ToList();
    }
}