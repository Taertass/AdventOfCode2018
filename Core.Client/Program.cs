using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Core.Client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Type[] allSolutions = Assembly.Load("Core").GetTypes()
              .Where(t => string.Equals(t.Namespace, "Core.Solutions", StringComparison.Ordinal))
              .OrderByDescending(t => t.Name)
              .ToArray();

            DayBase solution = (DayBase)Activator.CreateInstance(allSolutions[0], new object[0]);

            List<TestDataSets> allTestDataSets = solution.GetTestDataSets();

            int? specificSolution = null;
            int? specificCase = null;

            ConsoleColor color = Console.ForegroundColor;
            Console.WriteLine($"Running tests for {solution.GetType().Name}");
            Console.WriteLine();

            if (specificSolution != null && specificCase != null)
            {
                RunOne(solution, allTestDataSets, specificSolution.Value, specificCase.Value);
            }
            else
            {
                RunAll(solution, allTestDataSets);

                Console.ForegroundColor = color;

                Console.WriteLine();
                Console.WriteLine("----------------------- 0 -----------------------");
                Console.WriteLine();

                solution.SolveAndPrint();
            }

            Console.ReadLine();
        }

        private static void RunOne(DayBase solution, List<TestDataSets> allTestDataSets, int solutionNumber, int caseNumber)
        {
            TestDataSet testDataSet = allTestDataSets[solutionNumber - 1][caseNumber - 1];

            string result = solution.Solve(solutionNumber, testDataSet.Input);

            if (result == testDataSet.Result)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[Success] Test solution: {solutionNumber} case: {caseNumber}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[Failed] test solution: {solutionNumber} case: {caseNumber}");

                Console.WriteLine($"-= Expected =-");
                Console.WriteLine($"{testDataSet.Result}");

                Console.WriteLine($"-= Got =-");
                Console.WriteLine($"{result}");
            }
        }

        private static void RunAll(DayBase solution, List<TestDataSets> allTestDataSets)
        {
            for (int solutionNumber = 1; solutionNumber <= allTestDataSets.Count; solutionNumber++)
            {
                for (int caseNumber = 1; caseNumber <= allTestDataSets[solutionNumber - 1].Count; caseNumber++)
                {
                    RunOne(solution, allTestDataSets, solutionNumber, caseNumber);
                }
            }
        }
    }
}
