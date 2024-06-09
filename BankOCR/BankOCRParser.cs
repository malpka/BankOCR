using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOCR
{
    public class BankOCRParser
    {
        public string Parse(string input, bool tryToFixErrOrIll = true)
        {
            var verificationResult = VerifyInput(input);
            if (!verificationResult)
                throw new BankOCRException("Incorrect input");

            throw new NotImplementedException();
        }

        private bool VerifyInput(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }
            string[] lines = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            if (lines.Length != 4)
            {
                return false;
            }
            if (lines.Any(l => l.Length != 27))
            {
                return false;
            }
            if (!lines[3].Trim().Equals(string.Empty))
            {
                return false;
            }
            return true;
        }
    }
}
