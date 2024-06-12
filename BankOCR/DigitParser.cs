namespace BankOCR
{
    public class DigitParser
    {
        /// <summary>
        /// Parses a string digit representation into a single character.
        /// Parsing result must be an exact match.
        /// </summary>
        /// <param name="input">string input, can be flat without newlines, or with newlines</param>
        /// <returns>digit character, a result of parsing, or '?' if digit cannot be recognized</returns>
        /// <exception cref="BankOCRException"></exception>
        public char ParseExact(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new BankOCRException("Digit for parsing is empty");
            input = input.ReplaceLineEndings("");
            if (input.Length != 9)
                throw new BankOCRException("Digit for parsing has wrong length");
            for (var i = 0; i < Digit.Digits.Length; i++)
            {
                if (Digit.Digits[i].Equals(input))
                {
                    return (char)((int)'0' + i);
                }
            }
            return '?';
        }

        /// <summary>
        /// Parses a string digit representation into a set of possible characters using exact match, 
        /// or detection of missing / added single pipe or underscore
        /// </summary>
        /// <param name="input"></param>
        /// <returns>array of possible chars</returns>
        public char[] Parse(string input)
        {
            var resultSet = new HashSet<char>();

            var basicResult = ParseExact(input);
            if(basicResult != '?')
                resultSet.Add(basicResult);

            /* Valid characters and positions are based on DIGIT_8
             * 123  _
             * 456 |_|
             * 789 |_|
             * The error may be:
             * - a missing bar '|','_' then it may be placed only on a valid position, using DIGIT_8 as a base for valid bar positions
             * - an excesive bar - can be placed on any of 9 places, trying to replace it with space
             */
            var validBars = Digit.DIGIT_8
                .ToCharArray()
                .Select((character, index) => new { character, index })
                .Where(e => e.character != ' ');

            var validBlanks = Digit.DIGIT_8
                .ToCharArray()
                .Select((character, index) => new { character = ' ', index });

            foreach (var digitBar in validBars.Concat(validBlanks))
            {
                var mutated = Mutate(input, digitBar.index, digitBar.character);
                var parseResult = mutated == null ? '?' : ParseExact(mutated);
                if (parseResult != '?')
                {
                    resultSet.Add(parseResult);
                }
            }

            return resultSet.ToArray();
        }

        /// <summary>
        /// Modifies input string by changing character on position with a replacement value
        /// </summary>
        /// <param name="input"></param>
        /// <param name="position"></param>
        /// <param name="replacement"></param>
        /// <returns></returns>
        private string? Mutate(string input, int position, char replacement)
        {
            if (input[position].Equals(replacement))
                return null; // little optimisation to prevent excessive ParseExact calls
            var mutated = input.ToCharArray();
            mutated[position] = replacement;
            return new string(mutated);
        }
    }
}
