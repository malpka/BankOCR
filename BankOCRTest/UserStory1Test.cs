using BankOCR;

namespace BankOCRTest
{
    public class UserStory1Test
    {
        [Fact]
        public void UserStory1_TestCase1_ShouldParseCorrectly()
        {
            var inputData =
@"
 _  _  _  _  _  _  _  _  _ 
| || || || || || || || || |
|_||_||_||_||_||_||_||_||_|";
            var outputData = "000000000";

            var parser = new BankOCRParser();
            var parseResult = parser.Parse(inputData);

            Assert.Equal(parseResult, outputData);              
        }

        [Theory]
        [MemberData(nameof(UserStory1TestData))]
        public void UserStory1TestScenarios_ShouldParseCorrectly(string inputData, string expected)
        {
            var parser = new BankOCRParser();
            var parseResult = parser.Parse(inputData, tryToFixErrOrIll: false);
            Assert.Equal(expected, parseResult);
        }

        public static IEnumerable<object[]> UserStory1TestData()
        {
            yield return new object[] { @"
 _  _  _  _  _  _  _  _  _ 
| || || || || || || || || |
|_||_||_||_||_||_||_||_||_|
                           ", "000000000" };

            yield return new object[] { @"
  |  |  |  |  |  |  |  |  |
  |  |  |  |  |  |  |  |  |
                           ", "111111111" };
            yield return new object[] { @"
 _  _  _  _  _  _  _  _  _ 
 _| _| _| _| _| _| _| _| _|
|_ |_ |_ |_ |_ |_ |_ |_ |_ 
                           ", "222222222" };

yield return new object[] { @"
 _  _  _  _  _  _  _  _  _ 
 _| _| _| _| _| _| _| _| _|
 _| _| _| _| _| _| _| _| _|
                           ", "333333333" };

yield return new object[] { @"
                           
|_||_||_||_||_||_||_||_||_|
  |  |  |  |  |  |  |  |  |
                           ", "444444444" };

yield return new object[] { @"
 _  _  _  _  _  _  _  _  _ 
|_ |_ |_ |_ |_ |_ |_ |_ |_ 
 _| _| _| _| _| _| _| _| _|
                           ", "555555555" };

yield return new object[] { @"
 _  _  _  _  _  _  _  _  _ 
|_ |_ |_ |_ |_ |_ |_ |_ |_ 
|_||_||_||_||_||_||_||_||_|
                           ", "666666666" };

yield return new object[] { @"
 _  _  _  _  _  _  _  _  _ 
  |  |  |  |  |  |  |  |  |
  |  |  |  |  |  |  |  |  |
                           ", "777777777" };

yield return new object[] { @"
 _  _  _  _  _  _  _  _  _ 
|_||_||_||_||_||_||_||_||_|
|_||_||_||_||_||_||_||_||_|
                           ", "888888888" };

yield return new object[] { @"
 _  _  _  _  _  _  _  _  _ 
|_||_||_||_||_||_||_||_||_|
 _| _| _| _| _| _| _| _| _|
                           ", "999999999" };

yield return new object[] { @"
    _  _     _  _  _  _  _ 
  | _| _||_||_ |_   ||_||_|
  ||_  _|  | _||_|  ||_| _|
                           ", "123456789" };

        }

    }
}