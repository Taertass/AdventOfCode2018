using System;
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

            List<string> candidates = new List<string>();
            foreach(char candidateChar in letters.ToCharArray())
            {
                var charArray = input.ToCharArray();
                StringBuilder sb = new StringBuilder();

                bool wasAnythingDestroyed = false;
                do
                {
                    for (int i = 0; i < charArray.Length; i++)
                    {
                        wasAnythingDestroyed = false;

                        char c1 = charArray[i];
                        if(c1 == '_')
                        {
                            continue;
                        }

                        if (char.ToLower(c1) != candidateChar)
                        {
                            sb.Append(c1);
                            continue;
                        }

                        bool shouldSkip = false;
                        for (int j = 0; j < charArray.Length; j++)
                        {
                            char c2 = charArray[j];

                            if(char.IsUpper(c1))
                                shouldSkip = char.IsLower(c2) && char.ToLower(c1) == char.ToLower(c2);
                            else
                                shouldSkip = char.IsUpper(c2) && char.ToLower(c1) == char.ToLower(c2);

                            if(shouldSkip)
                            {
                                charArray[j] = '_';
                                break;
                            }
                        }

                        if (shouldSkip)
                        {
                            wasAnythingDestroyed = true;

                            charArray[i] = '_';
                        }
                        else
                        {
                            sb.Append(c1);
                        }
                    }


                } while (wasAnythingDestroyed);


                candidates.Add(sb.ToString());
            }


            var tested = new List<string>();
            foreach(var candidate in candidates)
            {
                tested.Add(Solve1(candidate));
            }


            var shortestCandiates = tested.OrderBy(c => Convert.ToInt32(c)).First();
            return shortestCandiates;
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
