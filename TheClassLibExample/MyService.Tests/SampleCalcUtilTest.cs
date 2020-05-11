using System;
using Xunit;

// https://github.com/dotnet/samples/tree/master/core/getting-started/unit-testing-using-dotnet-test
namespace MyService.Tests
{
    public class SampleCalcUtilTest
    {
        [Fact]
        public void BasicTest()
        {
            Assert.True(SampleCalcUtil.IsEven(0), "0 should be even");
            Assert.False(SampleCalcUtil.IsEven(1), "1 should not be even");
            Assert.True(SampleCalcUtil.IsEven(2), "2 should be even");
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(1)]
        [InlineData(3)]
        public void DataDrivenTest(int value)
        {
            var result = SampleCalcUtil.IsEven(value);

            Assert.False(result, $"{value} should not be even");
        }
    }
}
