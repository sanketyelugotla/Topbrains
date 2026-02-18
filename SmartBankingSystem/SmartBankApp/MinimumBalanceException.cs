using System;
using System.Collections.Generic;
using System.Text;

namespace SmartBankApp
{
    public class MinimumBalanceException : Exception
    {
        public MinimumBalanceException(string message) : base(message) { }
    }
}
