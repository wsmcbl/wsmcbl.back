using System.Globalization;
using wsmcbl.src.model.accounting;

namespace wsmcbl.src.dto.accounting;

public class TariffDto
{
    public int tariffId { get; set; }
    public string schoolYear { get; set; }
    public string concept { get; set; }
    public float amount { get; set; }
    public string? dueDate { get; set; }
    public bool isLate { get; set; }
    public int type { get; set; }
    public int educationalLevel { get; set; }

    public TariffDto(TariffEntity entity)
    {
        tariffId = entity.tariffId;
        schoolYear = entity.schoolyearId!;
        concept = entity.concept;
        amount = entity.amount;
        dueDate = entity.getDateString();
        isLate = entity.isLate;
        type = entity.type;
        educationalLevel = entity.educationalLevel;
    }
}