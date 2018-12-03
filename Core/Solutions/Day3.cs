using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Solutions
{
    public class Day3 : DayBase
    {
        private class Claim
        {
            public int Id { get; set; }

            public int X { get; set; }

            public int Y { get; set; }

            public int Width { get; set; }

            public int Height { get; set; }

            public Claim(string values)
            {
                string[] elements = values.Split(' ').Select(v => v.Trim()).ToArray();

                string idText = elements[0].Substring(1);
                string[] xy = elements[2].Substring(0, elements[2].Length - 1).Split(',');
                string[] widthHeight = elements[3].Split('x');

                Id = Convert.ToInt32(idText);
                X = Convert.ToInt32(xy[0]);
                Y = Convert.ToInt32(xy[1]);
                Width = Convert.ToInt32(widthHeight[0]);
                Height = Convert.ToInt32(widthHeight[1]);
            }
        }

        private class Grid
        {
            public int Width { get; }
            public int Height { get; }

            private string[,] _grid = new string[1000,1000];

            public Grid(int width, int height)
            {
                Width = width;
                Height = height;

                _grid = new string[Width, Height];
            }

            public void ApplyClaim(Claim claim)
            {
                for(int x = claim.X; x < claim.X + claim.Width; x++)
                {
                    for(int y = claim.Y; y < claim.Y + claim.Height; y++)
                    {
                        _grid[y, x] = _grid[y, x] == null ? claim.Id.ToString() : "X";
                    }
                }
            }

            public int GetDoubledArea()
            {
                int doubledArea = 0;
                for (int x = 0; x < Width; x++)
                {
                    for (int y = 0; y < Height; y++)
                    {
                        string value = _grid[x, y];
                        if (value == "X")
                        {
                            doubledArea++;
                        }
                    }
                }

                return doubledArea;
            }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                for (int x = 0; x < Width; x++)
                {
                    for (int y = 0; y < Height; y++)
                    {
                        sb.Append(_grid[x, y] ?? ".");
                    }
                    sb.AppendLine();
                }

                return sb.ToString();
            }

            internal bool HasOverlap(Claim claim)
            {
                bool hasOverlap = false;

                for (int x = claim.X; x < claim.X + claim.Width; x++)
                {
                    for (int y = claim.Y; y < claim.Y + claim.Height; y++)
                    {
                        if (_grid[y, x] == "X")
                            hasOverlap = true;
                    }
                }

                return hasOverlap;
            }
        }

        public override string Solve1(string input)
        {
            string [] lines = SplitByNewlineAsString(input);
            List<Claim> claims = lines.Select(l => new Claim(l)).ToList();

            int width = claims.Select(c => c.Y + c.Width).Max();
            int height = claims.Select(c => c.X + c.Height).Max();

            Grid grid = new Grid(width, height);
            foreach (Claim claim in claims)
            {
                grid.ApplyClaim(claim);
            }
            
            return grid.GetDoubledArea().ToString();
        }

        public override string Solve2(string input)
        {
            string[] lines = SplitByNewlineAsString(input);
            List<Claim> claims = lines.Select(l => new Claim(l)).ToList();

            int width = claims.Select(c => c.Y + c.Width).Max();
            int height = claims.Select(c => c.X + c.Height).Max();

            Grid grid = new Grid(width, height);
            foreach (Claim claim in claims)
            {
                grid.ApplyClaim(claim);
            }

            List<Claim> foundClaims = new List<Claim>();
            foreach (Claim claim in claims)
            {
                bool hasOverlap = grid.HasOverlap(claim);
                if(!hasOverlap)
                {
                    foundClaims.Add(claim);
                }
            }

            return foundClaims.First().Id.ToString();
        }

        public override List<TestDataSets> GetTestDataSets()
        {
            List<TestDataSets> testDataSets = new List<TestDataSets>()
            {
                new TestDataSets()
                {
                    new TestDataSet()
                    {
                        Input =
@"#1 @ 1,3: 4x4
#2 @ 3,1: 4x4
#3 @ 5,5: 2x2",
                        Result = "4"         
                    },
                    new TestDataSet()
                    {
                        Input =
@"#1 @ 1,3: 4x4
#2 @ 3,1: 4x4
#3 @ 5,4: 2x2",
                        Result = "6"
                    },
                    new TestDataSet()
                    {
                        Input =
@"#1 @ 1,3: 5x5
#2 @ 3,1: 4x4",
                        Result = "6"
                    },
                    new TestDataSet()
                    {
                        Input =
@"#1 @ 1,3: 5x5
#2 @ 3,1: 5x5",
                        Result = "9"
                    }
                }
            };

            return testDataSets;
        }
    }
}
