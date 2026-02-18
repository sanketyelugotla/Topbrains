using System;
using System.Collections.Generic;
using System.Text;

namespace SmartBankApp
{
    public class InsufficientBalanceException : Exception
    {
        public InsufficientBalanceException(string message) : base(message) {}
    }
}
