using wsmcbl.src.model.accounting;

namespace wsmcbl.src.dto.accounting;

public class InvoiceDetailDto
{
    public int tariffId { get; set; }
    public string schoolYear { get; set; } = null!;
    public string concept { get; set; } = null!;
    public float amount { get; set; }
    public float? discount { get; set; }
    public float arrears { get; set; }
    public bool itPaidLate { get; set; }

    public InvoiceDetailDto()
    {
    }

    public InvoiceDetailDto(TransactionTariffEntity transaction, StudentEntity student)
    {
        tariffId = transaction.tariffId;
        concept = transaction.concept();
        amount = transaction.officialAmount();
        arrears = transaction.calculateArrear();
        discount = student.calculateDiscount(transaction.officialAmount());
        itPaidLate = transaction.itPaidLate();
        schoolYear = transaction.schoolYear();
    }
    
}