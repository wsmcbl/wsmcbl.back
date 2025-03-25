using wsmcbl.src.model.accounting;

namespace wsmcbl.src.dto.accounting;

public class DebtDto
{
    public int tariffId { get; set; }
    public string? schoolyearId { get; set; }
    public string? concept { get; set; }
    public decimal subTotal { get; set; }
    public decimal arrears { get; set; }
    public decimal discount { get; set; }
    public decimal total { get; set; }
    public decimal debtBalance { get; set; }
    public bool itPaid { get; set; }

    public DebtDto(DebtHistoryEntity entity)
    {
        tariffId = entity.tariffId;
        schoolyearId = entity.schoolyear;
        concept = entity.tariff.concept;
        subTotal = entity.subAmount;
        discount = subTotal != 0 ? entity.calculateDiscount() : 0;
        arrears = entity.arrears;
        total = entity.amount;
        debtBalance = entity.getDebtBalance();
        itPaid = entity.isPaid;
    }
}