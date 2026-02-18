namespace FlexibleInventorySystem.Domain
{
    public class ElectronicsProduct : Product
    {
        public string Brand { get; private set; }
        public string Model { get; private set; }
        public int WarrantyPeriodMonths { get; private set; }
        public int PowerUsageWatts { get; private set; }
        public System.DateTime ManufacturingDate { get; private set; }

        public ElectronicsProduct(string name, string sku, decimal price, int quantityInStock,
                                  string brand, string model, int warrantyPeriodMonths, int powerUsageWatts, System.DateTime manufacturingDate)
            : base(name, sku, price, quantityInStock)
        {
            Brand = brand;
            Model = model;
            WarrantyPeriodMonths = warrantyPeriodMonths;
            PowerUsageWatts = powerUsageWatts;
            ManufacturingDate = manufacturingDate;
        }

        public override string GetCategory()
        {
            return "Electronics";
        }

        public override void Validate()
        {
            if (string.IsNullOrWhiteSpace(Brand)) throw new System.InvalidOperationException("Brand cannot be empty.");
            if (string.IsNullOrWhiteSpace(Model)) throw new System.InvalidOperationException("Model cannot be empty.");
            if (WarrantyPeriodMonths < 0) throw new System.InvalidOperationException("Warranty period cannot be negative.");
            if (PowerUsageWatts < 0) throw new System.InvalidOperationException("Power usage cannot be negative.");
            if (ManufacturingDate > System.DateTime.Now) throw new System.InvalidOperationException("Manufacturing date cannot be in the future.");
        }
    }
}