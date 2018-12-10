using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Solutions
{
    public class Day5 : DayBase
    {
        public override string Solve1(string input)
        {
            string result = input;
            
            bool wasAnythingDestroyed = false;
            do
            {
                wasAnythingDestroyed = false;
                var charArray = result.ToCharArray();
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < charArray.Length; i++)
                {
                    char c1 = charArray[i];
                    if (i + 1 == charArray.Length)
                    {
                        sb.Append(c1);
                        continue;
                    }
                    char c2 = charArray[i + 1];

                    bool shouldSkip = ((char.IsLower(c1) && char.IsUpper(c2)) || char.IsLower(c2) && char.IsUpper(c1)) && char.ToLower(c1) == char.ToLower(c2);
                    if (!shouldSkip)
                    {
                        sb.Append(c1);
                    }
                    else
                    {
                        i++; //Skip
                        wasAnythingDestroyed = true;
                    }
                }

                result = sb.ToString();

            } while (wasAnythingDestroyed);
            
            return result.Length.ToString();
        }

        public override string Solve2(string input)
        {
            string result = input;

            string letters = "abcdefghijklmnopqrstuvwxyz";

            foreach(char candidateChar in letters.ToCharArray())
            {
                var charArray = input.ToCharArray();

                bool wasAnythingDestroyed = false;
                do
                {



                } while(wasAnythingDestroyed)
                for (int i = 0; i < charArray.Length; i++)
                {
                    char c1 = charArray[i];
                    if (!char.IsLower(c1) || c1 != candidateChar)
                        continue;
                    
                    bool shouldSkip = false;
                    for (int j = 0; j < charArray.Length; j++)
                    {
                        char c2 = charArray[j];
                        shouldSkip = char.IsUpper(c2) && char.ToLower(c1) == char.ToLower(c2);
                    }

                    if (shouldSkip)
                    {
                        wasAnythingDestroyed = true;
                        charArray.RemoveAt(i);
                    }
                    else
                    {
                        sb.Append(c1);
                    }
                }
            }




            
            return result.Length.ToString();
        }

        public override List<TestDataSets> GetTestDataSets()
        {
            List<TestDataSets> testDataSets = new List<TestDataSets>()
            {
                new TestDataSets()
                {
                    new TestDataSet()
                    {
                        Input = "aA",
                        Result = "0"
                    },
                    new TestDataSet()
                    {
                        Input = "abBA",
                        Result = "0"
                    },
                    new TestDataSet()
                    {
                        Input = "abAB",
                        Result = "4"
                    },
                    new TestDataSet()
                    {
                        Input = "aabAAB",
                        Result = "6"
                    },
                    new TestDataSet()
                    {
                        Input = "dabAcCaCBAcCcaDA",
                        Result = "10"
                    }
                },
                new TestDataSets
                {
                    new TestDataSet()
                    {
                        Input = "dabAcCaCBAcCcaDA",
                        Result = "4"
                    }
                }
            };

            return testDataSets;
        }
    }
}
