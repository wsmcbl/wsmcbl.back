using wsmcbl.src.model.accounting;

namespace wsmcbl.src.dto.accounting;

public class TariffDto
{
    public int tariffId { get; set; }
    public string schoolyearId { get; set; }
    public string concept { get; set; }
    public decimal amount { get; set; }
    public DateOnlyDto? dueDate { get; set; }
    public bool isLate { get; set; }
    public int type { get; set; }
    public int educationalLevel { get; set; }

    public TariffDto(TariffEntity entity)
    {
        tariffId = entity.tariffId;
        schoolyearId = entity.schoolyearId!;
        concept = entity.concept;
        amount = (decimal)entity.amount;
        dueDate = entity.dueDate == null ? null : new DateOnlyDto((DateOnly)entity.dueDate!);
        isLate = entity.isLate;
        type = entity.type;
        educationalLevel = entity.educationalLevel;
    }
}