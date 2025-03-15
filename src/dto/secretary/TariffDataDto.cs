using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.secretary;

public class TariffDataDto : IBaseDto<TariffDataEntity>
{
    public int tariffDataId { get; set; }
    public string concept { get; set; } = null!;
    public int typeId { get; set; }
    public float amount { get; set; }
    public bool isActive { get; set; }
    public DateOnlyDto? dueDate { get; set; }
    public int educationalLevel { get; set; }
    
    public TariffDataEntity toEntity()
    {
        var entity = new TariffDataEntity
        {
            concept = concept,
            amount = amount,
            typeId = typeId,
            educationalLevel = educationalLevel,
            isActive = isActive
        };

        if (dueDate != null)
        {
            entity.dueDate = dueDate.toEntity();
        }

        return entity;
    }
}