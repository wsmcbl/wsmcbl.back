namespace wsmcbl.src.dto.output;

public class DetailDto
{
    public int tariffId { get; set; }
    
    public string schoolYear { get; set; } = null!;
    
    public string concept { get; set; } = null!;
    
    public float amount { get; set; }

    public float? discount { get; set; }

    public float arrears { get; set; }
    
    public float subTotal { get; set; }
    
    public bool itPaidLate { get; set; }

    public void computeDiscount()
    {
        discount = amount*(1 - discount);
    }

    public void computeArrears()
    {
        arrears = (float)(itPaidLate ? amount * 0.1 : 0);
    }
}