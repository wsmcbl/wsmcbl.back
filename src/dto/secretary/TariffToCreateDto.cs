using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using wsmcbl.src.model.accounting;

namespace wsmcbl.src.dto.secretary;

public class TariffToCreateDto : IBaseDto<TariffEntity>
{
    [Required] public string schoolYear { get; set; } = null!;
    [Required] public string concept { get; set; } = null!;
    [JsonRequired] public float amount { get; set; }
    public DateOnlyDto? dueDate { get; set; }
    [JsonRequired] public int type { get; set; }
    [JsonRequired] public int modality { get; set; }

    public TariffToCreateDto()
    {
    }

    public TariffToCreateDto(TariffEntity tariff)
    {
        schoolYear = tariff.schoolYear!;
        concept = tariff.concept;
        amount = tariff.amount;
        type = tariff.type;
        modality = tariff.educationalLevel;

        if (tariff.dueDate != null)
        {
            dueDate = new DateOnlyDto((DateOnly)tariff.dueDate);
        }
    }

    public TariffEntity toEntity()
    {
        var tariff = new TariffEntity
        {
            schoolYear = schoolYear,
            concept = concept,
            amount = amount,
            type = type,
            educationalLevel = modality
        };

        if (dueDate != null)
        {
            tariff.dueDate = dueDate.toEntity();
        }

        return tariff;
    }
}