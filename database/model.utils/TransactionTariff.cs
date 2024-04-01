using wsmcbl.back.model.accounting;

namespace wsmcbl.back.database.model.utils;

public class TransactionTariff
{
    public string transactionId { get; }
    public int tariffId { get; }

    public TransactionEntity transaction { get; set; }
    public TariffEntity tariff { get; set; }
}