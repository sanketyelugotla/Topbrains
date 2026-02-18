using System;
using System.Collections.Generic;
using System.Text;

namespace SmartBankApp
{
    public class LoanAccount : BankAccount
    {
        private double InterestRate = 0.05;

        public override void CalculateInterest()
        {
            double interest = InterestRate * Balance;
            Balance -= interest;

            Console.WriteLine($"interest deducted from balance");
        }
    }
}
