public class FestivalDiscount : IDiscountStrategy
{
    public double DiscountPercentage { get; set; }

    public FestivalDiscount(double discountPercentage)
    {
        DiscountPercentage = discountPercentage;
    }

    public double ApplyDiscount(double originalPrice)
    {
        return originalPrice - (originalPrice * (DiscountPercentage / 100));
    }
}