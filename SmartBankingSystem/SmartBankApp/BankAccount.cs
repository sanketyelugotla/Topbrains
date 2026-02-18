using System;
using System.Collections.Generic;
using System.Text;

namespace SmartBankApp
{
    public abstract class BankAccount
    {
        public string AccNo { get; set; }
        public string CustomerName { get; set; }
        public double Balance { get; set; }

        public virtual void Deposit(double amount)
        {
            Balance += amount;
            Console.WriteLine($"{amount} deposited");
        }
        public virtual void Withdraw(double amount)
        {
            if(Balance < amount)
            {
                throw new InsufficientBalanceException("insufficient balance");
            }
        }
        public abstract void CalculateInterest();
    }
}
