using wsmcbl.src.model.accounting;

namespace wsmcbl.src.dto.secretary;

public class TariffDto
{
    public string concept { get; set; } = null!;
    public decimal amount { get; set; }
    public DateOnlyDto? dueDate { get; set; }
    public int typeId { get; set; }
    public int educationalLevel { get; set; }

    public TariffDto()
    {
    }

    public TariffDto(TariffEntity tariff)
    {
        concept = tariff.concept;
        amount = (decimal)tariff.amount;
        typeId = tariff.type;
        educationalLevel = tariff.educationalLevel;

        if (tariff.dueDate != null)
        {
            dueDate = new DateOnlyDto((DateOnly)tariff.dueDate);
        }
    }

    public TariffEntity toEntity()
    {
        var tariff = new TariffEntity
        {
            concept = concept,
            amount = (float)amount,
            type = typeId,
            educationalLevel = educationalLevel,
            isLate = false
        };

        if (dueDate != null)
        {
            tariff.dueDate = dueDate.toEntity();
        }

        return tariff;
    }
}