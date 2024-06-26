using BankOCR;

namespace BankOCRTest
{
    public class UserStory3Test
    {
        [Theory]
        [MemberData(nameof(UserStory3TestData))]
        public void UserStory3TestScenarios_ShouldParseCorrectly(string inputData, string expected)
        {
            var parser = new LineParser(new ParserOptions()
            {
                TryToFixErrOrIll = false,
                ReportIllAccount = true,
                ReportErrAccount = true
            });
            var parseResult = parser.Parse(inputData.TrimStart(Environment.NewLine.ToCharArray()));
            Assert.Equal(expected, parseResult);
        }

        // Remark: below every test data item contains 1 additional empty line on the beginning, so it needs to be removed before processing
        public static IEnumerable<object[]> UserStory3TestData()
        {
            yield return new object[] { @"
 _  _  _  _  _  _  _  _    
| || || || || || || ||_   |
|_||_||_||_||_||_||_| _|  |
                           ", "000000051" };

            yield return new object[] { @"
    _  _  _  _  _  _     _ 
|_||_|| || ||_   |  |  | _ 
  | _||_||_||_|  |  |  | _|
                           ", "49006771? ILL" };
            yield return new object[] { @"
    _  _     _  _  _  _  _ 
  | _| _||_| _ |_   ||_||_|
  ||_  _|  | _||_|  ||_| _ 
                           ", "1234?678? ILL" };

            yield return new object[] { @"
    _  _  _  _  _  _  _  _ 
|_||_   ||_ | ||_|| || || |
  | _|  | _||_||_||_||_||_|
                           ", "457508000" };

            yield return new object[] { @"
 _  _     _  _        _  _ 
|_ |_ |_| _|  |  ||_||_||_ 
|_||_|  | _|  |  |  | _| _|
                           ", "664371495 ERR" };

            yield return new object[] { @"
 _  _        _  _  _  _  _ 
|_||_   |  || ||_|| | _||_ 
|_||_|  |  ||_||_ | | _||_|
                           ", "86110??36 ILL" };

        }

    }
}