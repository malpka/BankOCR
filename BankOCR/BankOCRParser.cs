﻿using System;
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

            var result = string.Empty;

            var inputLines = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).Take(3);
            for (var i = 0; i < 9; i++)
            {
                var inputDigitFlatten = string.Join("", inputLines.Select(s => s.Substring(i * 3, 3)));
                var digitParseResult = ParseDigit(inputDigitFlatten);
                result += digitParseResult;
            }

            return result;
        }

        public char ParseDigit(string digit)
        {
            if (string.IsNullOrEmpty(digit))
                throw new BankOCRException("Digit for parsing is empty");
            digit = digit.ReplaceLineEndings("");
            for(var i = 0; i < Digit.Digits.Length; i++)
            {
                if (Digit.Digits[i].Equals(digit))
                {
                    return (char)((int)'0' + i);
                }
            }
            return 'c';
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
