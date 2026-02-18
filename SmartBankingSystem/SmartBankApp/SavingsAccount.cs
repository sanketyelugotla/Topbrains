using System;
using System.Collections.Generic;
using System.Text;

namespace SmartBankApp
{
    public class SavingsAccount : BankAccount
    {
        private double MinimumBalance = 100;
        private double InterestRate = 0.02;
        public override void CalculateInterest()
        {
            double interest = Balance * InterestRate;
            Balance -= interest;
            Console.WriteLine($"interest deducted from balance");
        }
        public override void Withdraw(double amount)
        {
            if(Balance - amount < MinimumBalance)
            {
                double maxWithdraw = Balance - MinimumBalance;
                throw new MinimumBalanceException($"max amount possible to withdraw: {maxWithdraw} to maintain min balance");
            }
            base.Withdraw(amount);
        }

    }
}
