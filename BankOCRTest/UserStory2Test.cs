using BankOCR;

namespace BankOCRTest
{
    public class UserStory2Test
    {
        [Theory]
        [MemberData(nameof(UserStory2TestData))]
        public void UserStory2TestScenarios_ShouldParseCorrectly(string inputData, bool expected)
        {
            var parser = new LineParser(new ParserOptions() { TryToFixErrOrIll = false });
            var parseResult = parser.Parse(inputData.TrimStart(Environment.NewLine.ToCharArray()));
            var accountVerifier = new AccountVerifier(parseResult);
            var checksumVerificationResult = accountVerifier.Verify();

            Assert.Equal(expected, checksumVerificationResult);
        }

        // Remark: below every test data item contains 1 additional empty line on the beginning, so it needs to be removed before processing
        public static IEnumerable<object[]> UserStory2TestData()
        {
            yield return new object[] { @"
 _  _  _  _  _  _  _  _  _ 
| || || || || || || || || |
|_||_||_||_||_||_||_||_||_|
                           ", (0 + 2 * 0 + 3 * 0 + 4 * 0 + 5 * 0 + 6 * 0 + 7 * 0 + 8 * 0 + 9 * 0) % 11 == 0 };

            yield return new object[] { @"
                           
  |  |  |  |  |  |  |  |  |
  |  |  |  |  |  |  |  |  |
                           ", (1 + 2 * 1 + 3 * 1 + 4 * 1 + 5 * 1 + 6 * 1 + 7 * 1 + 8 * 1 + 9 * 1) % 11 == 0 };
            yield return new object[] { @"
 _  _  _  _  _  _  _  _  _ 
 _| _| _| _| _| _| _| _| _|
|_ |_ |_ |_ |_ |_ |_ |_ |_ 
                           ", (2 + 2 * 2 + 3 * 2 + 4 * 2 + 5 * 2 + 6 * 2 + 7 * 2 + 8 * 2 + 9 * 2) % 11 == 0 };

            yield return new object[] { @"
 _  _  _  _  _  _  _  _  _ 
 _| _| _| _| _| _| _| _| _|
 _| _| _| _| _| _| _| _| _|
                           ", (3 + 2 * 3 + 3 * 3 + 4 * 3 + 5 * 3 + 6 * 3 + 7 * 3 + 8 * 3 + 9 * 3) % 11 == 0 };

            yield return new object[] { @"
                           
|_||_||_||_||_||_||_||_||_|
  |  |  |  |  |  |  |  |  |
                           ", (4 + 2 * 4 + 3 * 4 + 4 * 4 + 5 * 4 + 6 * 4 + 7 * 4 + 8 * 4 + 9 * 4) % 11 == 0 };

            yield return new object[] { @"
 _  _  _  _  _  _  _  _  _ 
|_ |_ |_ |_ |_ |_ |_ |_ |_ 
 _| _| _| _| _| _| _| _| _|
                           ", (5 + 2 * 5 + 3 * 5 + 4 * 5 + 5 * 5 + 6 * 5 + 7 * 5 + 8 * 5 + 9 * 5) % 11 == 0 };

            yield return new object[] { @"
 _  _  _  _  _  _  _  _  _ 
|_ |_ |_ |_ |_ |_ |_ |_ |_ 
|_||_||_||_||_||_||_||_||_|
                           ", (6 + 2 * 6 + 3 * 6 + 4 * 6 + 5 * 6 + 6 * 6 + 7 * 6 + 8 * 6 + 9 * 6) % 11 == 0 };

            yield return new object[] { @"
 _  _  _  _  _  _  _  _  _ 
  |  |  |  |  |  |  |  |  |
  |  |  |  |  |  |  |  |  |
                           ", (7 + 2 * 7 + 3 * 7 + 4 * 7 + 5 * 7 + 6 * 7 + 7 * 7 + 8 * 7 + 9 * 7) % 11 == 0 };

            yield return new object[] { @"
 _  _  _  _  _  _  _  _  _ 
|_||_||_||_||_||_||_||_||_|
|_||_||_||_||_||_||_||_||_|
                           ", (8 + 2 * 8 + 3 * 8 + 4 * 8 + 5 * 8 + 6 * 8 + 7 * 8 + 8 * 8 + 9 * 8) % 11 == 0 };

            yield return new object[] { @"
 _  _  _  _  _  _  _  _  _ 
|_||_||_||_||_||_||_||_||_|
 _| _| _| _| _| _| _| _| _|
                           ", (9 + 2 * 9 + 3 * 9 + 4 * 9 + 5 * 9 + 6 * 9 + 7 * 9 + 8 * 9 + 9 * 9) % 11 == 0 };

            yield return new object[] { @"
    _  _     _  _  _  _  _ 
  | _| _||_||_ |_   ||_||_|
  ||_  _|  | _||_|  ||_| _|
                           ", (9 + 2 * 8 + 3 * 7 + 4 * 6 + 5 * 5 + 6 * 4 + 7 * 3 + 8 * 2 + 9 * 1) % 11 == 0 };

        }

    }
}