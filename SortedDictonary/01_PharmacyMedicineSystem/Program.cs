using System;
using Services;
using Domain;
using Exceptions;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var util = new MedicineUtility();

            while (true)
            {
                Console.WriteLine("1 → Display all medicines (sorted by expiry year)");
                Console.WriteLine("2 → Update medicine price");
                Console.WriteLine("3 → Add medicine");
                Console.WriteLine("4 → Exit");
                Console.Write("Enter your choice: ");

                int choice = Convert.ToInt16(Console.ReadLine());

                try
                {
                    switch (choice)
                    {
                        case 1:
                            var all = util.GetAllMedicines();
                            foreach (var m in all)
                            {
                                Console.WriteLine($"Details: {m.Id} {m.Name} {m.Price} {m.ExpiryYear}");
                            }
                            break;
                        case 2:
                            Console.Write("Enter Id to update price: ");
                            var id = Console.ReadLine();
                            Console.Write("Enter new price: ");
                            int newPrice = Convert.ToInt16(Console.ReadLine());
                            util.UpdateMedicinePrice(id, newPrice);
                            Console.WriteLine("Price updated.");
                            break;
                        case 3:
                            Console.WriteLine("Enter medicine as: MedicineID Name Price ExpiryYear");
                            var line = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(line)) { Console.WriteLine("Invalid input."); break; }
                            var parts = line.Split(' ');
                            if (parts.Length != 4) { Console.WriteLine("Input must have 4 parts."); break; }
                            var med = new Medicine { Id = parts[0], Name = parts[1], Price = Convert.ToInt16(parts[2]), ExpiryYear = Convert.ToInt16(parts[3]) };
                            util.AddMedicine(med);
                            Console.WriteLine("Medicine added.");
                            break;
                        case 4:
                            return;
                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                }
                catch (DuplicateMedicineException d)
                {
                    Console.WriteLine(d.Message);
                }
                catch (InvalidPriceException p)
                {
                    Console.WriteLine(p.Message);
                }
                catch (InvalidExpiryYearException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (MedicineNotFoundException n)
                {
                    Console.WriteLine(n.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}
