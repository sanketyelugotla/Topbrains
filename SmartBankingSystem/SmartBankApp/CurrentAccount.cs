using System;
using System.Collections.Generic;
using System.Text;

namespace SmartBankApp
{
    public class CurrentAccount : BankAccount
    {
        public double OverdraftLimit { get; set; } = 1000;

        public override void CalculateInterest()
        {
            return;
        }

        public override void Withdraw(double amount)
        {
            if(Balance + OverdraftLimit < amount)
            {
                throw new InsufficientBalanceException("overdraft limit exceeded");
            }
            Balance -= amount;
        }


    }
}
