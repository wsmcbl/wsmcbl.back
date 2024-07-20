using System.ComponentModel.DataAnnotations;
using wsmcbl.src.model.accounting;

namespace wsmcbl.src.dto.input;

public class TariffDto : IBaseDto<TariffEntity>
{
    [Required] public string schoolYear { get; set; }
    [Required] public string concept { get; set; }
    public float amount { get; set; }
    public DateOnlyDto? dueDate { get; set; }
    public int type { get; set; }
    public int modality { get; set; }

    public TariffEntity toEntity()
    {
        var tariff = new TariffEntity
        {
            schoolYear = schoolYear,
            concept = concept,
            amount = amount,
            type = type,
            modality = modality
        };

        if (dueDate != null)
        {
            tariff.dueDate = dueDate.toEntity();
        }

        return tariff;
    }

    internal static TariffDto init(TariffEntity tariff)
    {
        var dto = new TariffDto
        {
            schoolYear = tariff.schoolYear,
            concept = tariff.concept,
            amount = tariff.amount,
            type = tariff.type,
            modality = tariff.modality,
        };

        if (tariff.dueDate != null)
        {
            dto.dueDate = DateOnlyDto.init((DateOnly)tariff.dueDate);
        }

        return dto;
    }
}