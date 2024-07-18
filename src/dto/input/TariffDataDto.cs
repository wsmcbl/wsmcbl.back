using System.ComponentModel.DataAnnotations;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.input;

public class TariffDataDto : IBaseDto<TariffDataEntity>
{
    [Required] public string concept { get; set; }
    public float amount { get; set; }
    public DateOnlyDto dueDate { get; set; }
    public int typeId { get; set; }
    public int modality { get; set; }
    
    public TariffDataEntity toEntity()
    {
        return new TariffDataEntity
        {
            concept = concept,
            amount = amount,
            dueDate = dueDate.toEntity(),
            typeId = typeId,
            modality = modality
        };
    }
}