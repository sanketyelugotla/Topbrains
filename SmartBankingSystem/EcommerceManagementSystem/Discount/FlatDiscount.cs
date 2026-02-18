public class FlatDiscount : IDiscountStrategy
{
    public double DiscountAmount { get; set; }

    public FlatDiscount(double discountAmount)
    {
        DiscountAmount = discountAmount;
    }

    public double ApplyDiscount(double originalPrice)
    {
        return originalPrice - DiscountAmount;
    }
}