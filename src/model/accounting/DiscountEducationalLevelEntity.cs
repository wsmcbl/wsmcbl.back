namespace wsmcbl.src.model.accounting;

public class DiscountEducationalLevelEntity
{
    public int discountEducationalLeveLId { get; set; }
    public int discountId { get; set; }
    public int educationalLevel { get; set; }
    public float amount { get; set; }
    
    public int getDiscountIdFormat()
    {
        return discountId switch
        {
            < 3 => 1,
            > 3 and <= 6 => 2,
            > 6 and < 10 => 3,
            _ => discountId
        };
    }
}