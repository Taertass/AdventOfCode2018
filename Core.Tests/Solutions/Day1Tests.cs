using Xunit;

namespace Core.Solutions
{
    public class Day1Tests
    {
        [Theory]
        [InlineData(1, 1)]
        [InlineData(1, 2)]
        [InlineData(1, 3)]
        [InlineData(2, 1)]
        [InlineData(2, 2)]
        [InlineData(2, 3)]
        [InlineData(2, 4)]
        public void Should_solve1(int solution, int testCase)
        {
            //Arrange
            Day1 unit = new Day1();

            TestDataSet testDataSet = unit.GetTestDataSet(solution, testCase);

            if(testDataSet != null)
            {
                string result = unit.Solve(solution, testDataSet.Input);
                Assert.Equal(testDataSet.Result, result);
            } 
            else
            {
                Assert.True(false, $"No data set for solution: {solution} testCase: {testCase}");
            }
        }
    }
}
