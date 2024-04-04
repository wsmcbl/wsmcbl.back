using wsmcbl.back.model.accounting;

namespace wsmcbl.back.database.utils;

public class TransactionTariff
{
    public int tariffid { get; set; }
    public string transactionid { get; set; }
    
    public TariffEntity tariff { get; set; }    
    public TransactionEntity transaction { get; set; }
}