public class OrderItem
{
    public Product? Product { get; set; }
    public int Quantity { get; set; }
    public double TotalPrice()
    {
        return Product?.Price * Quantity ?? 0;
    }

    public OrderItem(Product product, int quantity)
    {
        Product = product;
        Quantity = quantity;
    }
}