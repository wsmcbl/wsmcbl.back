using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.secretary;

public class TariffDataDto : IBaseDto<TariffDataEntity>
{
    [Required] public string concept { get; set; } = null!;
    [JsonRequired] public float amount { get; set; }
    public DateOnlyDto? dueDate { get; set; }
    [JsonRequired] public int typeId { get; set; }
    [JsonRequired] public int modality { get; set; }
    
    public TariffDataEntity toEntity()
    {
        var entity = new TariffDataEntity
        {
            concept = concept,
            amount = amount,
            typeId = typeId,
            modality = modality
        };

        if (dueDate != null)
        {
            entity.dueDate = dueDate.toEntity();
        }

        return entity;
    }
}