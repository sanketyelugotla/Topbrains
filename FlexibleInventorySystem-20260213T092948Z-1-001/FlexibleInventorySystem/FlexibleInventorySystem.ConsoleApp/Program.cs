using System;
using FlexibleInventorySystem.Services;
using FlexibleInventorySystem.Domain;

namespace FlexibleInventorySystem.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IInventoryService inventoryService = new InventoryService();

            Console.WriteLine("Flexible Inventory System");
            Console.WriteLine("--------------------------");

            try
            {
                // 1. Create Electronics Product
                var laptop = new ElectronicsProduct("Gaming Laptop", "ELEC-001", 1500.00m, 5, "TechBrand", "GamerPro X", 24, 250, DateTime.Now.AddMonths(-2));
                inventoryService.AddProduct(laptop);
                Console.WriteLine($"Added Electronics: {laptop.Name}");

                // 2. Create Grocery Product
                var milk = new GroceryProduct("Organic Milk", "GROC-001", 2.50m, 50, DateTime.Now.AddDays(7), 1.0, true, 4.0);
                inventoryService.AddProduct(milk);
                Console.WriteLine($"Added Grocery: {milk.Name}");

                // 3. Create Clothing Product
                var jeans = new ClothingProduct("Blue Jeans", "CLOTH-001", 45.00m, 20, "M", "Denim", "Men", "Blue");
                inventoryService.AddProduct(jeans);
                Console.WriteLine($"Added Clothing: {jeans.Name}");

                // 5. Retrieve and display
                Console.WriteLine("\n--- Inventory Report ---");
                
                var electronics = inventoryService.GetProductsByCategory<ElectronicsProduct>();
                foreach (var p in electronics)
                {
                    Console.WriteLine($"[Electronics] {p.Name} - Brand: {p.Brand}, Model: {p.Model}, Price: {p.Price:C}");
                }

                var groceries = inventoryService.GetProductsByCategory<GroceryProduct>();
                foreach (var p in groceries)
                {
                    Console.WriteLine($"[Grocery] {p.Name} - Expiry: {p.ExpiryDate.ToShortDateString()}, Organic: {p.IsOrganic}, Price: {p.Price:C}");
                }

                var clothing = inventoryService.GetProductsByCategory<ClothingProduct>();
                foreach (var p in clothing)
                {
                    Console.WriteLine($"[Clothing] {p.Name} - Size: {p.Size}, Gender: {p.Gender}, Price: {p.Price:C}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}