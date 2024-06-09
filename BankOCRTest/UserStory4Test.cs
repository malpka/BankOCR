using BankOCR;

namespace BankOCRTest
{
    public class UserStory4Test
    {
        [Theory]
        [MemberData(nameof(UserStory4TestData))]
        public void UserStory4TestScenarios_ShouldParseCorrectly(string inputData, string expected)
        {
            var parser = new BankOCRParser();
            var parseResult = parser.Parse(inputData);
            Assert.Equal(expected, parseResult);
        }

        public static IEnumerable<object[]> UserStory4TestData()
        {
            yield return new object[] { @"
                           
  |  |  |  |  |  |  |  |  |
  |  |  |  |  |  |  |  |  |
                           ", "711111111" };

            yield return new object[] { @"
 _  _  _  _  _  _  _  _  _ 
  |  |  |  |  |  |  |  |  |
  |  |  |  |  |  |  |  |  |
                           ", "777777177" };
            yield return new object[] { @"
 _  _  _  _  _  _  _  _  _ 
 _|| || || || || || || || |
|_ |_||_||_||_||_||_||_||_|
                           ", "200800000" };

            yield return new object[] { @"
 _  _  _  _  _  _  _  _  _ 
 _| _| _| _| _| _| _| _| _|
 _| _| _| _| _| _| _| _| _|
                           ", "333393333" };

            yield return new object[] { @"
 _  _  _  _  _  _  _  _  _ 
|_||_||_||_||_||_||_||_||_|
|_||_||_||_||_||_||_||_||_|
                           ", "888888888 AMB ['888886888', '888888880', '888888988']" };

            yield return new object[] { @"
 _  _  _  _  _  _  _  _  _ 
|_ |_ |_ |_ |_ |_ |_ |_ |_ 
 _| _| _| _| _| _| _| _| _|
                           ", "555555555 AMB ['555655555', '559555555']" };

            yield return new object[] { @"
 _  _  _  _  _  _  _  _  _ 
|_ |_ |_ |_ |_ |_ |_ |_ |_ 
|_||_||_||_||_||_||_||_||_|
                           ", "666666666 AMB ['666566666', '686666666']" };

            yield return new object[] { @"
 _  _  _  _  _  _  _  _  _ 
|_||_||_||_||_||_||_||_||_|
 _| _| _| _| _| _| _| _| _|
                           ", "999999999 AMB ['899999999', '993999999', '999959999']" };

            yield return new object[] { @"
    _  _  _  _  _  _     _ 
|_||_|| || ||_   |  |  ||_ 
  | _||_||_||_|  |  |  | _|
                           ", "490067715 AMB ['490067115', '490067719', '490867715']" };

            yield return new object[] { @"
    _  _     _  _  _  _  _ 
 _| _| _||_||_ |_   ||_||_|
  ||_  _|  | _||_|  ||_| _|
                           ", "123456789" };

            yield return new object[] { @"
 _     _  _  _  _  _  _    
| || || || || || || ||_   |
|_||_||_||_||_||_||_| _|  |
                           ", "000000051" };

            yield return new object[] { @"
    _  _  _  _  _  _     _ 
|_||_|| ||_||_   |  |  | _ 
  | _||_||_||_|  |  |  | _|
                           ", "490867715" };

        }

    }
}