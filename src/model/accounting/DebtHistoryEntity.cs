using wsmcbl.src.exception;

namespace wsmcbl.src.model.accounting;

public class DebtHistoryEntity
{
    public string studentId { get; set; } = null!;
    public int tariffId { get; set; }
    public string schoolyear { get; set; } = null!;
    public float arrears { get; set; }
    public float subAmount { get; set; }
    public float amount { get; set; }
    public float debtBalance { get; set; }
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

    public float getDebtBalance()
    {
        var debt = amount - debtBalance;
        return debt > 0 ? debt : 0;
    }

    public void forgiveDebt()
    {
        if (isPaid)
        {
            throw new ConflictException("The debt is already paid.");
        }
        
        subAmount = 0;
        arrears = 0;
    }

    public void restoreDebt(float value)
    {
        debtBalance -= value;
    }
}