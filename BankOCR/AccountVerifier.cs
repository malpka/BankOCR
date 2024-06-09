using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOCR
{
    public class AccountVerifier
    {
        public string Account { get; set; }
        public AccountVerifier(string account)
        {
            Account = account;
        }

        public bool Verify()
        {
            if (string.IsNullOrEmpty(Account))
            {
                return false;
            }
            return VerifyChecksum(Account);
        }

        private int CalculateChecksum(string inputData)
        {
            int checksum = 0;
            for (int i = 0; i < 9; i++)
            {
                checksum += (9 - i) * (int)Char.GetNumericValue(inputData, i);
            }
            checksum = checksum % 11;
            return checksum;
        }
        private bool VerifyChecksum(string inputData)
        {
            return CalculateChecksum(inputData) == 0;
        }
    }
}
