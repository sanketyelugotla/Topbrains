public class PercentageDiscount : IDiscountStrategy
{
    public double DiscountPercentage { get; set; }

    public PercentageDiscount(double discountPercentage)
    {
        DiscountPercentage = discountPercentage;
    }

    public double ApplyDiscount(double originalPrice)
    {
        return originalPrice - (originalPrice * (DiscountPercentage / 100));
    }
}