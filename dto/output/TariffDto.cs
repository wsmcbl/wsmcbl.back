namespace wsmcbl.back.dto.output;

public class TariffDto
{
    public int tariffId { get; set; }
    
    public string schoolYear { get; set; } = null!;
    
    public string concept { get; set; } = null!;
    
    public float amount { get; set; }

    public float discount { get; set; }

    public float arrears { get; set; }
    
    public float subTotal { get; set; }
    
    public float debtBalance { get; set; }
    
    public bool itPaidLate { get; set; }
}