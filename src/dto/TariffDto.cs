using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using wsmcbl.src.model.accounting;

namespace wsmcbl.src.dto;

public class TariffDto : IBaseDto<TariffEntity>
{
    [Required] public string schoolYear { get; set; } = null!;
    [Required] public string concept { get; set; } = null!;
    [JsonRequired] public float amount { get; set; }
    public DateOnlyDto? dueDate { get; set; }
    [JsonRequired] public int type { get; set; }
    [JsonRequired] public int modality { get; set; }

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
    
    public class Builder
    {
        private readonly TariffDto? dto;
        public Builder(TariffEntity tariff)
        {
            dto = new TariffDto
            {
                schoolYear = tariff.schoolYear,
                concept = tariff.concept,
                amount = tariff.amount,
                type = tariff.type,
                modality = tariff.modality,
            };

            if (tariff.dueDate != null)
            {
                dto.dueDate = new DateOnlyDto((DateOnly)tariff.dueDate);
            }
        }

        public TariffDto build() => dto!;
    }
}