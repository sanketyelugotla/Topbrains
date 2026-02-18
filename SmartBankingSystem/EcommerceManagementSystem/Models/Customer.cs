public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    static List<Customer> customers = new List<Customer>();

    public void Register(string name)
    {
        Customer customer = new Customer
        {
            Id = customers.Count + 1,
            Name = name
        };
        customers.Add(customer);
    }
}
