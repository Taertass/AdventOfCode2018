using System.Collections.Generic;
using System.Linq;

namespace Core.Solutions
{
    public class Day2 : DayBase
    {
        public override string Solve1(string input)
        {
            string[] lines = SplitByNewlineAsString(input);

            int countOfDoubleLetters = 0;
            int counOfTripleLetters = 0;

            foreach(var line in lines)
            {
                var groupedLetters = line.ToCharArray().GroupBy(c => c).OrderByDescending(g => g.Count());
                countOfDoubleLetters += groupedLetters.Any(gl => gl.Count() == 2) ? 1 : 0;
                counOfTripleLetters += groupedLetters.Any(gl => gl.Count() == 3) ? 1 : 0;
            }
            return (countOfDoubleLetters * counOfTripleLetters).ToString();
        }

        public override string Solve2(string input)
        {
            string[] lines = SplitByNewlineAsString(input);
            List<char[]> charLines = lines.Select(l => l.ToCharArray()).ToList();

            List<string> foundWords = new List<string>();
            List<char> word1Difference = new List<char>();
            List<char> word2Difference = new List<char>();

            for (int i = 0; i < charLines.Count - 1; i++)
            {
                char[] lineCharArray1 = charLines[i];

                for (int j = i + 1; j < charLines.Count; j++)
                {
                    char[] lineCharArray2 = charLines[j];

                    word1Difference.Clear();
                    word2Difference.Clear();

                    for (int k = 0; k < lineCharArray1.Length; k++)
                    {

                        if(lineCharArray1[k] != lineCharArray2[k])
                        {
                            word1Difference.Add(lineCharArray1[k]);
                            word2Difference.Add(lineCharArray2[k]);
                        }
                    }

                    if (word1Difference.Count == 1 && word2Difference.Count == 1)
                    {
                        string candidateString = new string(lineCharArray1);
                        string candidateString2 = new string(lineCharArray2);
                        char differentChar = word1Difference.First();
                        char differentChar2 = word2Difference.First();
                        string newString = candidateString.Replace(differentChar.ToString(), "");
                        string newString2 = candidateString2.Replace(differentChar2.ToString(), "");
                        foundWords.Add(newString);
                    }
                }
            }

            return foundWords.First();
        }

        public override List<TestDataSets> GetTestDataSets()
        {
            List<TestDataSets> testDataSets = new List<TestDataSets>()
            {
                new TestDataSets()
                {
                    new TestDataSet
                    {
                        Result = "0",
                        Input = "abcdef",
                    },
                    new TestDataSet
                    {
                        Result = "1",
                        Input =
@"abbcde
abcccd",
                    },
                    new TestDataSet
                    {
                        Result = "2",
                        Input =
@"abbcde
bababc",
                    },
                    new TestDataSet
                    {
                        Result = "2",
                        Input =
@"abcccd
bababc",
                    },
                    new TestDataSet
                    {
                        Result = "4",
                        Input =
@"abbcde
abcccd
bababc",
                    },
                    new TestDataSet
                    {
                        Result = "12",
                        Input =
@"abcdef
bababc
abbcde
abcccd
aabcdd
abcdee
ababab",
                    },
                },
                new TestDataSets()
                {
                    new TestDataSet()
                    {
                        Result = "fgij",
                        Input =
@"abcde
fghij
klmno
pqrst
fguij
axcye
wvxyz"
                    }
                }
            };

            return testDataSets;
        }
    }
}
