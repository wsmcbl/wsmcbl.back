namespace wsmcbl.back.model.accounting;

public class DiscountEntity
{
    public int discountId { get; set; }

    public string description { get; set; } = null!;

    public float amount { get; set; }

    public string? tag { get; set; }
}