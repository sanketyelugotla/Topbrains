using System;

namespace BookStoreApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO:
            // 1. Read initial input
            // Format: BookID Title Price Stock
            Console.Write("Enter book Id: ");
            string bookId = Console.ReadLine();
            Console.Write("Enter book title: ");
            string title = Console.ReadLine();
            Console.Write("Enter book price: ");
            int price = int.Parse(Console.ReadLine());
            Console.Write("Enter book stock: ");
            int stock = int.Parse(Console.ReadLine());

            Book book = new Book
            {
                Id = bookId,
                Title = title,
                Price = price,
                Stock = stock
            };

            BookUtility utility = new BookUtility(book);

            while (true)
            {
                // TODO:
                // Display menu:
                // 1 -> Display book details
                // 2 -> Update book price
                // 3 -> Update book stock
                // 4 -> Exit
                Console.WriteLine("Menu:");
                Console.WriteLine("1 -> Display book details");
                Console.WriteLine("2 -> Update book price");
                Console.WriteLine("3 -> Update book stock");
                Console.WriteLine("4 -> Exit");
                
                Console.Write("Enter your choice: ");
                int choice = int.Parse(Console.ReadLine()); // TODO: Read user choice
                
                switch (choice)
                {
                    case 1:
                        utility.GetBookDetails();
                        break;

                    case 2:
                        // TODO:
                        // Read new price
                        Console.Write("Enter new price: ");
                        int newPrice = int.Parse(Console.ReadLine());
                        utility.UpdateBookPrice(newPrice);
                        break;

                    case 3:
                        // TODO:
                        // Read new stock
                        Console.Write("Enter new stock: ");
                        int newStock = int.Parse(Console.ReadLine());
                        utility.UpdateBookStock(newStock);
                        break;

                    case 4:
                        Console.WriteLine("Thank You");
                        return;

                    default:
                        // TODO: Handle invalid choice
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}
