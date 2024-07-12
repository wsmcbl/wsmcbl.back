namespace wsmcbl.src.dto.output;

public class DetailDto
{
    public int tariffId { get; set; }
    
    public string schoolYear { get; set; } = null!;
    
    public string concept { get; set; } = null!;
    
    public float amount { get; set; }

    public float? discount { get; set; }

    public float arrears { get; set; }
    
    public bool itPaidLate { get; set; }
}