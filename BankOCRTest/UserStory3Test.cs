using BankOCR;

namespace BankOCRTest
{
    public class UserStory3Test
    {
        [Theory]
        [MemberData(nameof(UserStory3TestData))]
        public void UserStory3TestScenarios_ShouldParseCorrectly(string inputData, string expected)
        {
            var parser = new BankOCRParser();
            var parseResult = parser.Parse(inputData.TrimStart(Environment.NewLine.ToCharArray()));
            Assert.Equal(expected, parseResult);
        }

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

        }

    }
}