using Nito.NormalizedJson;
using System;
using Xunit;

namespace NormalizedJson.UnitTests
{
    public class HelpersUnitTests
    {
        [Theory]
        [InlineData("0", "0")]
        [InlineData("-0", "-0")]
        [InlineData("1", "1")]
        [InlineData("-5", "-5")]
        [InlineData("10", "1e1")]
        [InlineData("11", "1.1e1")]
        [InlineData("13e0", "1.3e1")]
        [InlineData("100", "1e2")]
        [InlineData("11e-1", "1.1")]
        [InlineData("101", "1.01e2")]
        [InlineData("1001", "1.001e3")]
        [InlineData("0.00e013", "0")]
        [InlineData("-0.00e013", "-0")]
        [InlineData("-100.0100E+013", "-1.0001e15")]
        [InlineData("28446744073709551615", "2.8446744073709551615e19")] // 18446744073709551615 == UInt64.MaxValue
        [InlineData("28446744073709551615e-28446744073709551615", "2.8446744073709551615e-28446744073709551596")]
        public void NormalizeNumber_NormalizesNumbersAsExpected(string input, string expected)
        {
            var result = Helpers.NormalizeNumber(input);
            Assert.Equal(expected, result);

            double value1, value2;
            if (double.TryParse(input, out value1) && double.TryParse(expected, out value2))
                Assert.Equal(value1, value2);
        }
    }
}
