namespace SmartBankApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<BankAccount> accounts = new();
            accounts.Add(new SavingsAccount() { AccNo = "acc001", CustomerName = "harsha", Balance = 500000 });
            accounts.Add(new LoanAccount() { AccNo = "acc002", CustomerName = "elon", Balance = 17000 });
            accounts.Add(new SavingsAccount() { AccNo = "acc003", CustomerName = "vivian", Balance = 35000 });
            accounts.Add(new SavingsAccount() { AccNo = "acc004", CustomerName = "alex", Balance = 21000 });
            accounts.Add(new SavingsAccount() { AccNo = "acc005", CustomerName = "steve", Balance = 11000 });

            var highestBalAcc = accounts.OrderByDescending(a => a.Balance).First();
            double totalAmount = accounts.Sum(a => a.Balance);
            var topThreeAcc = accounts.OrderByDescending(a => a.Balance).Take(3).ToList();
            var groupAcc = accounts
                .GroupBy(a => a.GetType().Name)
                .Select(g => new {AccountType = g.Key, TotalBalance = g.Sum(acc => acc.Balance)});
            var rName = accounts.Where(a => a.CustomerName.StartsWith("R")).ToList();

            foreach (var account in topThreeAcc)
            {
                Console.WriteLine(account.CustomerName);
            }

            foreach(var obj in groupAcc)
            {
                Console.WriteLine($"Account Type: {obj.AccountType} | Total Balance = {obj.TotalBalance}");
            }

            BankAccount b = new SavingsAccount();


            //foreach (var g in groupAcc)
            //{
            //    Console.WriteLine(g.Key);

            //    double total = 0;
            //    foreach(var acc in g)
            //    {
            //        total += acc.Balance;
            //    }
            //    Console.WriteLine($"{total}");
            //}


        }
    }
}
