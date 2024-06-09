using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOCR
{
    public class ParserOptions
    {
        public bool TryToFixErrOrIll { get; set; } = true;
        public bool ReportErrAccount { get; set; } = false;
        public bool ReportIllAccount { get; set; } = false;
    }
}
