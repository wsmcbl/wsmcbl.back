using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using wsmcbl.src.model.accounting;

namespace wsmcbl.src.dto.accounting;

public class DetailDto : IBaseDto<TransactionTariffEntity>
{
    [Required] public int tariffId { get; set; }
    [JsonRequired] public float amount { get; set; }
    [JsonRequired] public bool applyArrears { get; set; }
    
    public TransactionTariffEntity toEntity()
    {
        return new TransactionTariffEntity
        {
            tariffId = tariffId,
            amount = amount
        };
    }

}