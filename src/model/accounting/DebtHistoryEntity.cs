using wsmcbl.src.exception;

namespace wsmcbl.src.model.accounting;

public class DebtHistoryEntity
{
    public string studentId { get; set; } = null!;
    public int tariffId { get; set; }
    public string schoolyear { get; set; } = null!;
    public decimal arrears { get; set; }
    public decimal subAmount { get; set; }
    public decimal amount { get; set; }
    public decimal debtBalance { get; set; }
    public bool isPaid { get; set; }
    
    public TariffEntity tariff { get; set; } = null!;

    public DebtHistoryEntity()
    {
    }
    
    public DebtHistoryEntity(string studentId, TariffEntity tariff)
    {
        this.studentId = studentId;
        tariffId = tariff.tariffId;
        subAmount = tariff.amount;
        schoolyear = string.Empty;
        arrears = 0;
        debtBalance = 0;
        isPaid = false;
    }
    
    public bool havePayments()
    {
        return debtBalance > 0;
    }

    public decimal getDebtBalance()
    {
        var debt = amount - debtBalance;
        return debt > 0 ? debt : 0;
    }

    public void forgiveDebt()
    {
        if (isPaid)
        {
            throw new UpdateConflictException("Debt","The debt is already paid.");
        }

        if (debtBalance > subAmount)
        {
            arrears = debtBalance - subAmount;
            return;
        }
        
        arrears = 0;
        subAmount = debtBalance;
    }

    public void restoreDebt(decimal value)
    {
        debtBalance -= value;
    }

    public decimal calculateDiscount()
    {
        var result = tariff.amount - subAmount;
        return result < 0 ? 0 : result;
    }
}