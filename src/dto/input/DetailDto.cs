using System.ComponentModel.DataAnnotations;
using wsmcbl.src.model.accounting;

namespace wsmcbl.src.dto.input;

public class DetailDto : IBaseDto<TransactionTariffEntity>
{
    [Required] public int tariffId { get; set; }
    public float amount { get; set; }
    public bool applyArrear { get; set; }
    
    public TransactionTariffEntity toEntity()
    {
        return new TransactionTariffEntity
        {
            tariffId = tariffId,
            amount = amount
        };
    }

}