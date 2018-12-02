using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Core
{

    public abstract class DayBase
    {
        public abstract string Solve1(string input);

        public abstract string Solve2(string input);

        public abstract List<TestDataSets> GetTestDataSets();

        public string Solve(int solution, string input)
        {
            if (solution == 1)
            {
                return Solve1(input);
            }
            else if (solution == 2)
            {
                return Solve2(input);
            }
            else
            {
                throw new ArgumentException("Can only be 1 or 2", nameof(solution));
            }
        }

        public TestDataSet GetTestDataSet(int solution, int testCase)
        {
            List<TestDataSets> allTestDataSets = GetTestDataSets();
            TestDataSets testDataSets = null;
            if (solution < allTestDataSets.Count)
                testDataSets = allTestDataSets[solution];

            TestDataSet testDataSet = null;
            if (testDataSets != null && testCase < testDataSets.Count)
                testDataSet = testDataSets[testCase];

            return testDataSet;
        }

        public string GetDataSet()
        {
            return File.ReadAllText(Path.Combine("DataSets", $"{GetType().Name}.txt"));
        }

        public void SolveAndPrint()
        {
            Console.WriteLine($"Calucating solution for {GetType().Name}");

            string dataSet = null;
            try
            {
                Console.WriteLine("Loading data...");
                dataSet = GetDataSet();
            }
            catch { }

            if (string.IsNullOrWhiteSpace(dataSet))
            {
                Console.WriteLine($"    No data set found for {GetType().Name}.txt");
            }
            else
            {
                string result1 = "Unknown";
                string result2 = "Unknown";

                try
                {
                    Console.WriteLine("Calculating solution 1...");
                    result1 = Solve1(dataSet);
                }
                catch { }
                try
                {
                    Console.WriteLine("Calculating solution 2...");
                    result2 = Solve2(dataSet);
                }
                catch { }

                Console.WriteLine();
                Console.WriteLine($"Solution for {GetType().Name}");
                Console.WriteLine($"    Solution1: {result1}");
                Console.WriteLine($"    Solution2: {result2}");
            }

            Console.WriteLine();
        }

        protected string[] SplitByNewlineAsString(string input)
        {
            return input.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        }

        protected int[] SplitByNewlineAsInt(string input)
        {
            return SplitByNewlineAsString(input).Select(i => Convert.ToInt32(i.Trim())).ToArray();
        }

        protected long[] SplitByNewlineAsLong(string input)
        {
            return SplitByNewlineAsString(input).Select(i => Convert.ToInt64(i.Trim())).ToArray();
        }

        protected double[] SplitByNewlineAsDouble(string input)
        {
            return SplitByNewlineAsString(input).Select(i => Convert.ToDouble(i.Trim())).ToArray();
        }
    }
}
