using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Core.Solutions
{
    public class Day7 : DayBase
    {
        public class Instruction
        {
            public string Step1 { get; set; }

            public string Step2 { get; set; }

            public override string ToString()
            {
                return $"{Step1} -> {Step2}";
            }
        }

        public class Node
        {
            public bool IsExecuted { get; set; }

            public string Name { get; set; }

            public List<Node> Parents { get; set; }

            public List<Node> Children { get; set; }

            public List<Node> Descendants
            {
                get
                {
                    List<Node> descendants = new List<Node>();

                    descendants.AddRange(Children);

                    foreach(var node in Children)
                    {
                        descendants.AddRange(node.Descendants);
                    }
                    
                    return descendants;
                }
            }

            public List<Node> Preorder
            {
                get
                {
                    List<Node> descendants = new List<Node>();
                    descendants.Add(this);

                    foreach (var node in Children.OrderBy(c => c.Name))
                    {
                        descendants.AddRange(node.Preorder);
                    }

                    return descendants;
                }
            }

            public Node(string name)
            {
                Name = name;
                Parents = new List<Node>();
                Children = new List<Node>();
            }

            public override string ToString()
            {
                return Name;
            }


        }

        public override string Solve1(string input)
        {
            string[] lines = SplitByNewlineAsString(input);

            List<Instruction> instructions = new List<Instruction>();
            Dictionary<string, Node> nodes = new Dictionary<string, Node>();
            foreach (var line in lines)
            {
                var matches = Regex.Matches(line.ToLower(), "step [a-z]");

                var from = matches[0].Value.Split(' ')[1].Trim().ToUpper();
                var to = matches[1].Value.Split(' ')[1].Trim().ToUpper();

                //Find or build node
                nodes.TryGetValue(from, out Node fromNode);
                if(fromNode == null)
                {
                    fromNode = new Node(from);
                    nodes.Add(from, fromNode);
                }

                nodes.TryGetValue(to, out Node toNode);
                if (toNode == null)
                {
                    toNode = new Node(to);
                    nodes.Add(to, toNode);
                }

                fromNode.Children.Add(toNode);
                toNode.Parents.Add(fromNode);

                instructions = new List<Instruction>()
                {
                    new Instruction
                    {
                        Step1 = from,
                        Step2 = to
                    }
                };
            }

            StringBuilder executionOrder = new StringBuilder();

            
            Node node = nodes.Values.FirstOrDefault(n => n.Parents.Count == 0);

            foreach (var nodeToExecute in node.Preorder)
            {
                executionOrder.Append(nodeToExecute.Name);
            }

            //Queue<Node> nodesToExecute = new Queue<Node>();
            //nodesToExecute.Enqueue(node);



            //while(nodesToExecute.Any())
            //{
            //    Node nodeToExecute = nodesToExecute.Dequeue();
            //    executionOrder.Append(nodeToExecute.Name);
                
            //    foreach(Node childeNodesToExecute in nodeToExecute.Children.OrderBy(n => n.Name))
            //    {
            //        nodesToExecute.Enqueue(childeNodesToExecute);
            //    }
            //}

            return executionOrder.ToString();
        }

        public override string Solve2(string input)
        {
            return "";
        }

        public override List<TestDataSets> GetTestDataSets()
        {
            List<TestDataSets> testDataSets = new List<TestDataSets>()
            {
                new TestDataSets
                {
                    new TestDataSet
                    {
                        Result = "CABDFE",
                        Input =
@"Step C must be finished before step A can begin.
Step C must be finished before step F can begin.
Step A must be finished before step B can begin.
Step A must be finished before step D can begin.
Step B must be finished before step E can begin.
Step D must be finished before step E can begin.
Step F must be finished before step E can begin."
                    }
                }
            };

            return testDataSets;
        }
    }
}
