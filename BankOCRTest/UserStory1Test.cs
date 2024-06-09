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
    }
}