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
    
    public bool havePayments()
    {
        return debtBalance > 0;
    }

    public float getDebtBalance()
    {
        var debt = amount - debtBalance;
        return debt > 0 ? debt : 0;
    }

    public class Builder
    {
        private readonly DebtHistoryEntity entity;

        public Builder(string studentId, string schoolyear)
        {
            entity = new DebtHistoryEntity()
            {
                studentId = studentId,
                schoolyear = schoolyear
            };
        }

        public DebtHistoryEntity build() => entity;

        public Builder setTariffId(int tariffId)
        {
            entity.tariffId = tariffId;
            return this;
        }
    }
}