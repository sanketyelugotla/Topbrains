public class Order
{
    public int OrderId { get; set; }

    public Customer? Customer { get; set; }
    public List<OrderItem> Products { get; set; } = new List<OrderItem>();
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public string OrderStatus { get; set; } = string.Empty;
    internal IDiscountStrategy? DiscountStrategy { get; set; }
    static List<Order> orders = new List<Order>();

    public double CalculatePrice() {
        double totalPrice = 0;
        foreach (var item in Products)
        {
            totalPrice += item.TotalPrice();
        }
        if (DiscountStrategy != null)
        {
            totalPrice = DiscountStrategy.ApplyDiscount(totalPrice);
        }
        return totalPrice;
    }

    public void PlaceOrder(Customer customer, List<OrderItem> products)
    {
        
        Order order = new Order
        {
            OrderId = orders.Count + 1,
            Customer = customer,
            Products = products,
            OrderDate = DateTime.Now,
            OrderStatus = "Placed",
            DiscountStrategy = new FlatDiscount(10)
        };
        orders.Add(order);
    }
}