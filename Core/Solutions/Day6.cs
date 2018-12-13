using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Solutions
{
    public class Day6 : DayBase
    {
        public struct Coordinate
        {
            public int X { get; set; }

            public int Y { get; set; }

            public Coordinate(string data)
            {
                var parsedData = data.Split(',').Select(d => Convert.ToInt32(d.Trim())).ToList();
                X = parsedData[0];
                Y = parsedData[1];
            }
        }

        public class Node
        {
            public Coordinate Coordinate { get; set; }

            public string Name { get; set; }
        }

        public class NodeArea
        {
            public List<Coordinate> Coordinates { get; set; }

            public Node node { get; set; }

            public NodeArea()
            {
                Coordinates = new List<Coordinate>();
            }
        }

        public override string Solve1(string input)
        {
            string[] lines = SplitByNewlineAsString(input);
            List<Coordinate> coordinates = lines.Select(l => new Coordinate(l)).ToList();

            int width = coordinates.Max(c => c.X) + 1;
            int height = coordinates.Max(c => c.Y) + 1;

            List<Node> nodes = new List<Node>();
            string[,] grid = new string[width, height];
            char currentName = 'N';
            int index = 0;
            foreach(var coordinate in coordinates)
            {
                string nodeName = currentName.ToString() + index.ToString();
                grid[coordinate.X, coordinate.Y] = nodeName;

                nodes.Add(new Node()
                {
                    Coordinate = coordinate,
                    Name = nodeName
                });

                index++;
            }

            Dictionary<string, Node> indexedNodes = nodes.ToDictionary(n => n.Name.ToLower(), n => n);

            //populate grid
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    //Find closest Node
                    
                    var weightedNotes = nodes.Select(n => new { Node = n, Distance = Math.Abs(n.Coordinate.X - x) + Math.Abs(n.Coordinate.Y - y) }).OrderBy(n => n.Distance);
                    var closestNode = weightedNotes.First();

                    if (weightedNotes.Count(n => n.Distance == closestNode.Distance) > 1)
                        continue;
                    if (closestNode.Distance == 0)
                        continue;

                    grid[x, y] = closestNode.Node.Name.ToLower();
                }
            }

            //Sum Area
            Dictionary<string, NodeArea> nodeAreas = new Dictionary<string, NodeArea>();

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    string nodeName = grid[x, y]?.ToLower();
                    if (nodeName == null)
                        continue;

                    var node = indexedNodes[nodeName];

                    nodeAreas.TryGetValue(nodeName, out NodeArea nodeArea);
                    if(nodeArea == null)
                    {
                        nodeArea = new NodeArea()
                        {
                            node = node
                        };
                        nodeAreas.Add(nodeName, nodeArea);
                    }

                    nodeArea.Coordinates.Add(new Coordinate()
                    {
                        X = x,
                        Y = y
                    });
                }
            }

            List<NodeArea> nodeAreaCandidates = new List<NodeArea>();
            foreach (NodeArea nodeArea in nodeAreas.Values)
            {
                bool isInf = nodeArea.Coordinates.Any(n => (n.X == 0 || n.X == width - 1 || n.Y == 0 || n.Y == height - 1));

                if (!isInf)
                    nodeAreaCandidates.Add(nodeArea);
            }

            NodeArea maxNodeArea = nodeAreaCandidates.OrderByDescending(n => n.Coordinates.Count).First();

            //PRINT
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < height; i++)
            {

                for(int j = 0; j < width; j++)
                {
                    sb.Append(grid[j, i] ?? ".");
                }

                sb.AppendLine();
            }

            

            string output = sb.ToString();

            return maxNodeArea.Coordinates.Count.ToString();
        }

        public override string Solve2(string input)
        {
            string[] lines = SplitByNewlineAsString(input);
            List<Coordinate> coordinates = lines.Select(l => new Coordinate(l)).ToList();

            int width = coordinates.Max(c => c.X) + 1;
            int height = coordinates.Max(c => c.Y) + 1;

            List<Node> nodes = new List<Node>();
            string[,] grid = new string[width, height];
            char currentName = 'N';
            int index = 0;
            foreach (var coordinate in coordinates)
            {
                string nodeName = currentName.ToString() + index.ToString();
                grid[coordinate.X, coordinate.Y] = nodeName;

                nodes.Add(new Node()
                {
                    Coordinate = coordinate,
                    Name = nodeName
                });

                index++;
            }

            Dictionary<string, Node> indexedNodes = nodes.ToDictionary(n => n.Name.ToLower(), n => n);

            //populate grid
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    //Find closest Node

                    var weightedNotes = nodes.Select(n => new { Node = n, Distance = Math.Abs(n.Coordinate.X - x) + Math.Abs(n.Coordinate.Y - y) }).OrderBy(n => n.Distance);

                    var summedDistance = weightedNotes.Sum(n => n.Distance);

                    var closestNode = weightedNotes.First();

                    //if (weightedNotes.Count(n => n.Distance == closestNode.Distance) > 1)
                    //    continue;
                    //if (closestNode.Distance == 0)
                    //    continue;

                    //grid[x, y] = closestNode.Node.Name.ToLower();

                    if (summedDistance < 10000)
                    {
                        grid[x, y] = "#";
                    }
                }
            }

            //Sum Area

            indexedNodes["#"] = new Node();
            Dictionary<string, NodeArea> nodeAreas = new Dictionary<string, NodeArea>();

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    string nodeName = grid[x, y]?.ToLower();
                    if (nodeName == null)
                        continue;

                    var node = indexedNodes[nodeName];

                    nodeAreas.TryGetValue(nodeName, out NodeArea nodeArea);
                    if (nodeArea == null)
                    {
                        nodeArea = new NodeArea()
                        {
                            node = node
                        };
                        nodeAreas.Add(nodeName, nodeArea);
                    }

                    nodeArea.Coordinates.Add(new Coordinate()
                    {
                        X = x,
                        Y = y
                    });
                }
            }

            List<NodeArea> nodeAreaCandidates = new List<NodeArea>();
            foreach (NodeArea nodeArea in nodeAreas.Values)
            {
                bool isInf = nodeArea.Coordinates.Any(n => (n.X == 0 || n.X == width - 1 || n.Y == 0 || n.Y == height - 1));

                if (!isInf)
                    nodeAreaCandidates.Add(nodeArea);
            }

            NodeArea maxNodeArea = nodeAreaCandidates.OrderByDescending(n => n.Coordinates.Count).FirstOrDefault();
            if (maxNodeArea == null)
                return null;

            //PRINT
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < height; i++)
            {

                for (int j = 0; j < width; j++)
                {
                    sb.Append(grid[j, i] ?? ".");
                }

                sb.AppendLine();
            }


            string output = sb.ToString();

            return maxNodeArea.Coordinates.Count.ToString();
        }

        public override List<TestDataSets> GetTestDataSets()
        {
            List<TestDataSets> testDataSets = new List<TestDataSets>()
            {
                new TestDataSets()
                {
                    new TestDataSet()
                    {
                        Result = "17",
                        Input =
@"1, 1
1, 6
8, 3
3, 4
5, 5
8, 9",
                    }
                },
                new TestDataSets()
                {
                    new TestDataSet()
                    {
                        Result = "16",
                        Input =
@"1, 1
1, 6
8, 3
3, 4
5, 5
8, 9",
                    }
                }
            };

            return testDataSets;
        }
    }
}
