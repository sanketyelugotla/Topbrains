public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }
    public int Stock { get; set; }

    static List<Product> products = new List<Product>();
    static Dictionary<int, Product> productDictionary = new Dictionary<int, Product>();

    public void AddProduct(string name, double price, int stock)
    {
        Product product = new Product
        {
            Id = products.Count + 1,
            Name = name,
            Price = price,
            Stock = stock
        };
        products.Add(product);
        productDictionary[product.Id] = product;
    }

    public static IEnumerable<Product> GetAll()
    {
        return products;
    }

    public static Product? GetById(int id)
    {
        return productDictionary.TryGetValue(id, out var product) ? product : null;
    }

    public static bool ReduceStock(int id, int quantity)
    {
        var product = GetById(id);
        if (product == null) return false;
        if (product.Stock < quantity) return false;
        product.Stock -= quantity;
        return true;
    }
}