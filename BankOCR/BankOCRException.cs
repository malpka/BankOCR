using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOCR
{
    public class BankOCRException : Exception
    {
        public BankOCRException() { }
        public BankOCRException(string message) : base(message) { }

    }
}
