using wsmcbl.src.model.accounting;

namespace wsmcbl.src.dto.accounting;

public class DebtDto
{
    public int tariffId { get; set; }
    public string? schoolYear { get; set; }
    public string? concept { get; set; }
    public float total { get; set; }
    public float discount { get; set; }
    public float arrears { get; set; }
    public float subTotal { get; set; }
    public float debtBalance { get; set; }
    public bool itPaid { get; set; }

    public DebtDto()
    {
    }

    public DebtDto(DebtHistoryEntity entity)
    {
        tariffId = entity.tariffId;
        schoolYear = entity.schoolyear;
        concept = entity.tariff.concept;
        total = entity.amount;
        arrears = entity.arrears;
        subTotal = entity.subAmount;
        itPaid = entity.isPaid;
        debtBalance = entity.getDebtBalance();
        setDiscount(entity.subAmount);
    }
    
    private void setDiscount(float value)
    {
        discount = total - value;
    }
}