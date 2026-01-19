public class Account
{
    public string AccountNumber { get; set; }
    public decimal Balance { get; set; }
    public decimal Deposit(decimal amount)
    {
        try
        {
            if (amount > 0) Balance += amount;
            else throw new ArgumentException("Deposit amount must be positive.");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
        return Balance;
    }
    public decimal Withdraw(decimal amount)
    {
        try
        {
            if (amount <= 0) throw new ArgumentException("Withdrawal amount must be positive.");
            else if (amount > Balance) throw new InvalidOperationException("Insufficient funds.");
            else Balance -= amount;
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine(ex.Message);
        }
        return Balance;
    }
}

public class BankingApplication
{
    public static void Main(string[] args)
    {
        Account userAccount = new Account();
        Console.WriteLine("1. Deposit");
        Console.WriteLine("2. Withdraw");

        Console.WriteLine("Enter the choice");
        if (!int.TryParse(Console.ReadLine(), out int choice))
        {
            Console.WriteLine("Invalid choice. Please enter 1 or 2.");
            return;
        }

        Console.WriteLine("Enter the account number");
        userAccount.AccountNumber = Console.ReadLine();

        Console.WriteLine("Enter the balance");
        userAccount.Balance = ReadDecimalInput();

        if (choice == 1)
        {
            Console.WriteLine("Enter the amount to be deposit");
            decimal amount = ReadDecimalInput();
            Console.WriteLine("Balance amount " + userAccount.Deposit(amount));
        }
        else if (choice == 2)
        {
            Console.WriteLine("Enter the amount to be withdraw");
            decimal amount = ReadDecimalInput();
            Console.WriteLine("Balance amount " + userAccount.Withdraw(amount));
        }
    }

    // Helper method to ensure we get a valid number from the user
    private static decimal ReadDecimalInput()
    {
        decimal result;
        while (!decimal.TryParse(Console.ReadLine(), out result))
        {
            Console.WriteLine("Invalid input. Please enter a valid numeric amount:");
        }
        return result;
    }
}