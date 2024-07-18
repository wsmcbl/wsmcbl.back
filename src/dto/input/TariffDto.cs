using System.ComponentModel.DataAnnotations;
using wsmcbl.src.model.accounting;

namespace wsmcbl.src.dto.input;

public class TariffDto : IBaseDto<TariffEntity>
{
    [Required] public string schoolYear { get; set; }
    [Required] public string concept { get; set; }
    public float amount { get; set; }
    public DateOnlyDto dueDate { get; set; }
    public int type { get; set; }
    public int modality { get; set; }
    
    public TariffEntity toEntity()
    {
        return new TariffEntity
        {
            schoolYear = schoolYear,
            concept = concept,
            amount = amount,
            dueDate = dueDate.toEntity(),
            type = type,
            modality = modality
        };
    }
}