namespace wsmcbl.src.model.accounting;

public class DiscountEntity
{
    public int discountId { get; set; }
    public string description { get; set; } = null!;
    public string? tag { get; set; }
}