using wsmcbl.src.model.accounting;

namespace wsmcbl.src.dto.accounting;

public class PaymentItemDto
{
    public int tariffId { get; set; }
    public string schoolyearId { get; set; } = null!;
    public string concept { get; set; } = null!;
    public decimal amount { get; set; }
    public decimal discount { get; set; }
    public decimal arrears { get; set; }
    public decimal subTotal { get; set; }
    public decimal debtBalance { get; set; }
    public bool itPaidLate { get; set; }

    public PaymentItemDto()
    {
    }

    public PaymentItemDto(DebtHistoryEntity entity)
    {
        tariffId = entity.tariffId;
        concept = entity.tariff.concept;
        amount = entity.tariff.amount;
        itPaidLate = entity.tariff.isLate;
        schoolyearId = entity.tariff.schoolyearId!;
        arrears = entity.arrears;
        subTotal = entity.amount;
        debtBalance = entity.getDebtBalance();
        
        setDiscount(entity.subAmount);
    }
    
    private void setDiscount(decimal value)
    {
        discount = amount - value;
    }
}