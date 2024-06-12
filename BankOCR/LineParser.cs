namespace BankOCR
{
    public class LineParser
    {
        private const int MAX_INPUT_LINE_LENGTH = 27;
        private const int VALID_INPUT_LINE_COUNT = 4;
        private const int ACCOUNT_DIGIT_COUNT = 9;

        private ParserOptions options;
        private DigitParser digitParser = new DigitParser();

        public LineParser()
        {
            options = new ParserOptions()
            {
                ReportErrAccount = false,
                ReportIllAccount = false,
                TryToFixErrOrIll = true
            };
        }

        public LineParser(ParserOptions options)
        {
            this.options = options;
        }

        /// <summary>
        /// Parses input string to an account
        /// </summary>
        /// <param name="input">string containing lines of text account representation, separated with NewLine character</param>
        /// <returns>Parsed account, optionally decorated with ILL, ERR or AMB [] suffixes, depending on parsing results</returns>
        /// <exception cref="BankOCRException"></exception>
        public string Parse(string input)
        {
            var verificationResult = VerifyInput(input);
            if (!verificationResult)
            {
                throw new BankOCRException("Incorrect input");
            }

            var result = string.Empty;

            var inputLines = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).Take(3); // skips 4th line as it should be empty

            // transform input into an list of flatten 3x3 character digits
            var inputDigitsFlatten = new List<string>();
            for (var i = 0; i < ACCOUNT_DIGIT_COUNT; i++)
            {
                var inputDigitFlatten = string.Join("", inputLines.Select(s => s.Substring(i * 3, 3)));
                inputDigitsFlatten.Add(inputDigitFlatten);
            }

            // parse exact all digits
            inputDigitsFlatten.ForEach(idf =>  result += digitParser.ParseExact(idf));

            bool illResult = false;
            bool errResult = false;
            bool checksumVerificationResult = false;
            if (result.Contains('?'))
            {
                illResult = true;
            }
            else
            {
                checksumVerificationResult = new AccountVerifier(result).Verify();
                if (!checksumVerificationResult)
                {
                    errResult = true;
                }
            }

            if (options.TryToFixErrOrIll)
            {
                if (illResult) // try to fix '?' characters
                {
                    var possibleValidAccounts = new List<string>();

                    var permutations = Enumerable.Range(0, ACCOUNT_DIGIT_COUNT).Select(i =>
                        {
                            if (result[i] != '?') return new [] { result[i] };
                            return digitParser.Parse(inputDigitsFlatten[i]);
                        }).ToList();

                    var possibleAccounts = permutations.Aggregate(new[] { "" }.AsEnumerable(),
                        (agg, arr) => agg.Join(arr, x => 1, x => 1, (i, j) => i.ToString() + j));

                    possibleValidAccounts.AddRange(possibleAccounts.Where(a => new AccountVerifier(a).Verify() == true));

                    switch(possibleValidAccounts.Count)
                    {
                        case 0: result += " ILL"; break;
                        case 1: result = possibleValidAccounts[0]; break;
                        default: result += $" AMB[{string.Join(",", possibleValidAccounts)}]"; break;
                    }
                }
                if (errResult) // try to fix incorrect checksum by fixing 1 digit only
                {
                    var possibleValidAccounts = new List<string>();
                    for (int i = 0; i < ACCOUNT_DIGIT_COUNT; i++)
                    {
                        var possibilities = digitParser.Parse(inputDigitsFlatten[i]);
                        foreach (var possibleChars in possibilities)
                        {
                            var copy = result.ToCharArray();
                            copy[i] = possibleChars;
                            var copyString = new string(copy);
                            bool copyVerificationResult = new AccountVerifier(copyString).Verify();
                            if (copyVerificationResult)
                            {
                                possibleValidAccounts.Add(copyString);
                            }
                        }
                    }
                    switch (possibleValidAccounts.Count)
                    {
                        case 0: result += " ERR"; break;
                        case 1: result = possibleValidAccounts[0]; break;
                        default: result += $" AMB [{string.Join(", ", possibleValidAccounts.OrderBy(s => s).Select(s => $"'{s}'"))}]"; break;
                    }
                }
            }
            else
            {
                if (options.ReportIllAccount)
                {
                    result += illResult ? " ILL" : string.Empty;
                }
                if (options.ReportErrAccount)
                {
                    result += errResult ? " ERR" : string.Empty;
                }
            }

            return result;
        }


        /// <summary>
        /// Performs basic input verificaion
        /// </summary>
        /// <param name="input"></param>
        /// <returns>true when valid, otherwise false</returns>
        private bool VerifyInput(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }
            string[] lines = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            if (lines.Length != VALID_INPUT_LINE_COUNT)
            {
                return false;
            }
            if (lines.Any(l => l.Length != MAX_INPUT_LINE_LENGTH))
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
