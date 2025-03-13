using wsmcbl.src.model.accounting;

namespace wsmcbl.src.dto.accounting;

public class PaymentItemDto
{
    public int tariffId { get; set; }
    public string schoolYear { get; set; } = null!;
    public string concept { get; set; } = null!;
    public float amount { get; set; }
    public float discount { get; set; }
    public float arrears { get; set; }
    public float subTotal { get; set; }
    public float debtBalance { get; set; }
    public bool itPaidLate { get; set; }
    
    public void setDiscount(float value)
    {
        discount = amount - value;
    }

    public PaymentItemDto()
    {
    }

    public PaymentItemDto(DebtHistoryEntity entity)
    {
        tariffId = entity.tariffId;
        concept = entity.tariff.concept;
        amount = entity.tariff.amount;
        itPaidLate = entity.tariff.isLate;
        schoolYear = entity.tariff.schoolyearId!;
        arrears = entity.arrears;
        subTotal = entity.amount;
        debtBalance = entity.getDebtBalance();
        
        setDiscount(entity.subAmount);
    }
}