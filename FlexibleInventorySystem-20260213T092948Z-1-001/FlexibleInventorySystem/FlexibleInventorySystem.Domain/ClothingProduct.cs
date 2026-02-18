namespace FlexibleInventorySystem.Domain
{
    public class ClothingProduct : Product
    {
        public string Size { get; private set; }
        public string FabricType { get; private set; }
        public string Gender { get; private set; }
        public string Color { get; private set; }

        public ClothingProduct(string name, string sku, decimal price, int quantityInStock,
                               string size, string fabricType, string gender, string color)
            : base(name, sku, price, quantityInStock)
        {
            Size = size;
            FabricType = fabricType;
            Gender = gender;
            Color = color;
        }

        public override string GetCategory()
        {
            return "Clothing";
        }

        public override void Validate()
        {
            if (string.IsNullOrWhiteSpace(Size)) throw new System.InvalidOperationException("Size is required.");
            if (string.IsNullOrWhiteSpace(FabricType)) throw new System.InvalidOperationException("Fabric type is required.");
            if (string.IsNullOrWhiteSpace(Gender)) throw new System.InvalidOperationException("Gender is required.");
            if (string.IsNullOrWhiteSpace(Color)) throw new System.InvalidOperationException("Color is required.");

            var validSizes = new[] { "S", "M", "L", "XL" };
            if (System.Array.IndexOf(validSizes, Size) == -1)
                throw new System.InvalidOperationException("Invalid size. Allowed: S, M, L, XL");

            var validGenders = new[] { "Men", "Women", "Unisex" };
            if (System.Array.IndexOf(validGenders, Gender) == -1)
                throw new System.InvalidOperationException("Invalid gender. Allowed: Men, Women, Unisex");
        }
    }
}