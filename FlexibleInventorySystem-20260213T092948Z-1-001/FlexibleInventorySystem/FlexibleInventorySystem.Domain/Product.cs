using System;

namespace FlexibleInventorySystem.Domain
{
    public abstract class Product
    {
        public Guid ProductId { get; protected set; }
        public string Name { get; protected set; }
        public string SKU { get; protected set; }
        public decimal Price { get; protected set; }
        public int QuantityInStock { get; protected set; }
        public DateTime CreatedDate { get; protected set; }

        protected Product(string name, string sku, decimal price, int quantityInStock)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be null or empty.", nameof(name));
            if (string.IsNullOrWhiteSpace(sku)) throw new ArgumentException("SKU cannot be null or empty.", nameof(sku));
            if (price < 0) throw new ArgumentOutOfRangeException(nameof(price), "Price cannot be negative.");
            if (quantityInStock < 0) throw new ArgumentOutOfRangeException(nameof(quantityInStock), "Quantity cannot be negative.");

            ProductId = Guid.NewGuid();
            Name = name;
            SKU = sku;
            Price = price;
            QuantityInStock = quantityInStock;
            CreatedDate = DateTime.UtcNow;
        }

        public abstract string GetCategory();
        public abstract void Validate();

        public virtual void UpdateStock(int quantity)
        {
            if (QuantityInStock + quantity < 0)
            {
                throw new InvalidOperationException("Stock cannot be negative.");
            }
            QuantityInStock += quantity;
        }
    }
}