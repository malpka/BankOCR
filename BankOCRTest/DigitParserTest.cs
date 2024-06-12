using BankOCR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOCRTest
{
    public class DigitParserTest
    {
        [Theory]
        [MemberData(nameof(DigitParserTestData))]
        public void DigitParser_ParseExact_WhenGivenInput_ShouldReturnExpectedParsedValue(string input, char expected)
        {
            var d = new DigitParser();
            var result = d.ParseExact(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void DigitParser_ParseExact_WhenGivenEmptyInput_ShouldThrowException()
        {
            var input = "";
            var d = new DigitParser();
            Assert.Throws<BankOCRException>(() => d.ParseExact(input));
        }

        [Fact]
        public void DigitParser_ParseExact_WhenGivenWrongLengthInput_ShouldThrowException()
        {
            var input = "test";
            var d = new DigitParser();
            Assert.Throws<BankOCRException>(() => d.ParseExact(input));
        }

        public static IEnumerable<object[]> DigitParserTestData()
        {
            yield return new object[] { 
                " _ " +
                "| |" + 
                "|_|", 
                '0' };

            yield return new object[] {
                "   " +
                "| |" +
                "|_|",
                '?' };

            yield return new object[] {
                " _ " +
                "|_|" +
                "|_|",
                '8' };

            yield return new object[] {
                " _ " +
                " _|" +
                " _|",
                '3' };

            yield return new object[] {
                "XXX" +
                "XXX" +
                "XXX",
                '?' };
        }
    }
}
