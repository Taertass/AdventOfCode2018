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

            ConsoleColor color = Console.ForegroundColor;
            Console.WriteLine($"Running tests for {solution.GetType().Name}");

            for (int solutionNumber = 0; solutionNumber < allTestDataSets.Count; solutionNumber++)
            {
                TestDataSets testDataSets = allTestDataSets[solutionNumber];

                for (int caseNumber = 0; caseNumber < testDataSets.Count; caseNumber++)
                {
                    TestDataSet testDataSet = testDataSets[caseNumber];

                    string result = solution.Solve(solutionNumber + 1, testDataSet.Input);

                    if (result == testDataSet.Result)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"[Success] Test solution: {solutionNumber + 1} case: {caseNumber + 1}");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"[Failed] test solution: {solutionNumber + 1} case: {caseNumber + 1}");

                        Console.WriteLine($"-= Expected =-");
                        Console.WriteLine($"{testDataSet.Result}");

                        Console.WriteLine($"-= Got =-");
                        Console.WriteLine($"{result}");
                    }
                }
            }

            Console.ForegroundColor = color;

            Console.WriteLine();
            Console.WriteLine("-----------------------");
            solution.SolveAndPrint();

            Console.ReadLine();
        }
    }
}
