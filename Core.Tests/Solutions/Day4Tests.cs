using Xunit;

namespace Core.Solutions
{
    public class Day4Tests
    {
        [Theory]
        [InlineData(1, 1)]
        public void Should_solve(int solution, int testCase)
        {
            //Arrange
            var unit = new Day4();

            TestDataSet testDataSet = unit.GetTestDataSet(solution - 1, testCase - 1);

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
