public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the E-commerce Management System!");

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\nMenu:\n1. List Products\n2. Place Order\n3. Exit");
            Console.Write("Choose an option: ");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    {
                        Console.WriteLine("Available Products:");
                        foreach (var p in Product.GetAll())
                        {
                            Console.WriteLine($"{p.Id}. {p.Name} - Price: {p.Price} - Stock: {p.Stock}");
                        }
                        break;
                    }
                case "2":
                    {
                        Console.Write("Enter customer name: ");
                        var custName = Console.ReadLine() ?? string.Empty;
                        var customer = new Customer { Id = 1, Name = custName };

                        var items = new List<OrderItem>();
                        while (true)
                        {
                            Console.Write("Enter product id (or 'done' to finish): ");
                            var pid = Console.ReadLine();
                            if (string.Equals(pid, "done", StringComparison.OrdinalIgnoreCase)) break;
                            if (!int.TryParse(pid, out var id))
                            {
                                Console.WriteLine("Invalid id, try again.");
                                continue;
                            }
                            var product = Product.GetById(id);
                            if (product == null)
                            {
                                Console.WriteLine("Product not found.");
                                continue;
                            }
                            Console.Write($"Enter quantity for {product.Name}: ");
                            var qin = Console.ReadLine();
                            if (!int.TryParse(qin, out var qty) || qty <= 0)
                            {
                                Console.WriteLine("Invalid quantity.");
                                continue;
                            }
                            if (product.Stock < qty)
                            {
                                Console.WriteLine($"Only {product.Stock} units available.\n");
                                continue;
                            }
                            items.Add(new OrderItem(product, qty));
                            Console.WriteLine("Item added.");
                        }

                        if (items.Count == 0)
                        {
                            Console.WriteLine("No items added. Order cancelled.");
                            break;
                        }

                        foreach (var it in items)
                        {
                            Product.ReduceStock(it.Product!.Id, it.Quantity);
                        }

                        var order = new Order();
                        order.PlaceOrder(customer, items);
                        Console.WriteLine($"Order placed. Total price: {order.CalculatePrice()}");
                        break;
                    }
                case "3":
                    {
                        exit = true;
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Invalid option.");
                        break;
                    }
            }
        }
    }
}