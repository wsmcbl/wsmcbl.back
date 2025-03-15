using wsmcbl.src.model.secretary;
using DateOnly = System.DateOnly;

namespace wsmcbl.src.dto.secretary;

public class TariffDataDto
{
    public int tariffDataId { get; set; }
    public string concept { get; set; } = null!;
    public decimal amount { get; set; }
    public DateOnlyDto? dueDate { get; set; }
    public int typeId { get; set; }
    public int educationalLevel { get; set; }
    public bool isActive { get; set; }
    
    public TariffDataEntity toEntity(int id = 0)
    {
        var entity = new TariffDataEntity
        {
            tariffDataId = id,
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

    public TariffDataDto()
    {
    }

    public TariffDataDto(TariffDataEntity value)
    {
        tariffDataId = value.tariffDataId;
        concept = value.concept;
        amount = value.amount;
        typeId = value.typeId;
        educationalLevel = value.educationalLevel;
        isActive = value.isActive;

        if (value.dueDate != null)
        {
            dueDate = new DateOnlyDto((DateOnly)value.dueDate);
        }
    }
}