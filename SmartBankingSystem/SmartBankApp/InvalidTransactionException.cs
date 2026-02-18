using System;
using System.Collections.Generic;
using System.Text;

namespace SmartBankApp
{
    public class InvalidTransactionException : Exception
    {
        public InvalidTransactionException(string message) : base(message) { }
    }
}
