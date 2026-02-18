using System;

namespace FlexibleInventorySystem.Domain
{
    public class GroceryProduct : Product
    {
        public DateTime ExpiryDate { get; private set; }
        public double WeightKg { get; private set; }
        public bool IsOrganic { get; private set; }
        public double StorageTemperature { get; private set; }

        public GroceryProduct(string name, string sku, decimal price, int quantityInStock,
                              DateTime expiryDate, double weightKg, bool isOrganic, double storageTemperature)
            : base(name, sku, price, quantityInStock)
        {
            ExpiryDate = expiryDate;
            WeightKg = weightKg;
            IsOrganic = isOrganic;
            StorageTemperature = storageTemperature;
        }

        public override string GetCategory()
        {
            return "Grocery";
        }

        public override void Validate()
        {
            if (ExpiryDate < DateTime.Now) throw new InvalidOperationException("Product is expired.");
            if (WeightKg <= 0) throw new InvalidOperationException("Weight must be positive.");
        }
    }
}