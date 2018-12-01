using System.Collections.Generic;
using System.Linq;

namespace Core.Solutions
{
    public class Day1 : DayBase
    {
        public override string Solve1(string input)
        {
            int[] values = SplitByNewlineAsInt(input);

            return values.Sum().ToString();
        }

        public override string Solve2(string input)
        {
            int[] values = SplitByNewlineAsInt(input);

            HashSet<int> seenSums = new HashSet<int>();

            int? firstSumsSeenTwice = null;
            int rollingSum = 0;
            seenSums.Add(rollingSum);
            while (firstSumsSeenTwice == null)
            {
                foreach (int value in values)
                {
                    rollingSum += value;

                    if (seenSums.Contains(rollingSum))
                    {
                        firstSumsSeenTwice = rollingSum;
                        break;
                    }

                    seenSums.Add(rollingSum);
                }
            }

            return firstSumsSeenTwice.ToString();
        }

        public override List<TestDataSets> GetTestDataSets()
        {
            List<TestDataSets> testDataSets = new List<TestDataSets>()
            {
                new TestDataSets()
                {
                    new TestDataSet
                    {
                        Result = "3",
                        Input = "+1\n+1\n+1",
                    },
                    new TestDataSet
                    {
                        Result = "0",
                        Input = "+1\n+1\n-2",
                    },
                    new TestDataSet
                    {
                        Result = "-6",
                        Input = "-1\n-2\n-3",
                    },
                },
                new TestDataSets()
                {
                    new TestDataSet
                    {
                        Result = "0",
                        Input = "+1\n-1",
                    },
                    new TestDataSet
                    {
                        Result = "10",
                        Input = "+3\n+3\n+4\n-2\n-4",
                    },
                    new TestDataSet
                    {
                        Result = "5",
                        Input = "-6\n+3\n+8\n+5\n-6",
                    },
                    new TestDataSet
                    {
                        Result = "14",
                        Input = "+7\n+7\n-2\n-7\n-4",
                    },
                }
            };

            return testDataSets;
        }
    }
}
