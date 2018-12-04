using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Core.Solutions
{
    public class Day4 : DayBase
    {
        public enum GuardEventType
        {
            None,
            ShiftBegins,
            FallsAsleep,
            WakesUp
        }

        public class EventTable
        {
            public List<GuardEvent> GuardEvents { get; } = new List<GuardEvent>();

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Date   ID   Minute");
                sb.Append("            ");

                for (var i = 0; i < 60; i++)
                {
                    sb.Append(i.ToString().Substring(0,1));
                    
                }
                sb.AppendLine();

                sb.Append("            ");

                for (var i = 0; i < 60; i++)
                {
                    string number = i.ToString();
                    if (number.Length < 2)
                        number = "0" + number;

                    sb.Append(number.ToString().Substring(1, 1));
                }
                sb.AppendLine();

                int currentGuardId = 0;
                foreach(var groupedGuardEvent in GuardEvents.OrderBy(ge => ge.TimeStamp))
                {
                    sb.Append(groupedGuardEvent.First().TimeStamp.ToString("MM-dd"));
                    sb.Append($"  #{groupedGuardEvent.First().Id}  ");

                    bool isOnShift = false;
                    bool isAwake = true;

                    for (var i = 0; i < 60; i++)
                    {
                        var guradEvent = groupedGuardEvent.FirstOrDefault(ge => ge.TimeStamp.Minute == i);

                        if(guradEvent?.GuardEventType == GuardEventType.ShiftBegins)
                        {
                            isOnShift = true;
                        }
                        else if (guradEvent?.GuardEventType == GuardEventType.FallsAsleep)
                        {
                            isAwake = false;
                        }
                        else if (guradEvent?.GuardEventType == GuardEventType.WakesUp)
                        {
                            isAwake = true;
                        }

                        if (isOnShift && isAwake)
                        {
                            sb.Append(".");
                        }
                        else if (isOnShift && !isAwake)
                        {
                            sb.Append("#");
                        }
                        else
                        {
                            sb.Append(" ");
                        }
                    }

                    sb.AppendLine();
                }
                
                return sb.ToString();
            }
        }

        public class GuardEvent
        {
            public GuardEventType GuardEventType { get; set; }

            public DateTime TimeStamp { get; set; }

            public string Data { get; set; }

            public int Id { get; set; }

            public GuardEvent(string data) : this(data, null)
            { }

            public GuardEvent(string data, int? guardId)
            {
                string datePart = data.Substring(1, 16);
                TimeStamp = DateTime.Parse(datePart);

                if (data.Contains("begins shift"))
                {
                    GuardEventType = GuardEventType.ShiftBegins;
                }
                else if (data.Contains("falls asleep"))
                {
                    GuardEventType = GuardEventType.FallsAsleep;
                }
                else if (data.Contains("wakes up"))
                {
                    GuardEventType = GuardEventType.WakesUp;
                }

                Id = guardId ?? 0;

                var match = Regex.Match(data, "Guard #[0-9]+");
                if (match.Success)
                {
                    var splitIndex = match.Value.IndexOf('#');
                    var idText = match.Value.Substring(splitIndex + 1, match.Value.Length - splitIndex - 1);
                    Id = Convert.ToInt32(idText);
                }

            }
        }

        public override string Solve1(string input)
        {
            EventTable eventTable = new EventTable();

            string[] lines = SplitByNewlineAsString(input);
            GuardEvent guard = null ;
            foreach (string line in lines)
            {
                guard = new GuardEvent(line, guard?.Id);
                eventTable.GuardEvents.Add(guard);
            }

            var s = eventTable.ToString();

            return "";
        }

        public override string Solve2(string input)
        {
            return "";
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
@"[1518-11-01 00:00] Guard #10 begins shift
[1518-11-01 00:05] falls asleep
[1518-11-01 00:25] wakes up
[1518-11-01 00:30] falls asleep
[1518-11-01 00:55] wakes up
[1518-11-01 23:58] Guard #99 begins shift
[1518-11-02 00:40] falls asleep
[1518-11-02 00:50] wakes up
[1518-11-03 00:05] Guard #10 begins shift
[1518-11-03 00:24] falls asleep
[1518-11-03 00:29] wakes up
[1518-11-04 00:02] Guard #99 begins shift
[1518-11-04 00:36] falls asleep
[1518-11-04 00:46] wakes up
[1518-11-05 00:03] Guard #99 begins shift
[1518-11-05 00:45] falls asleep
[1518-11-05 00:55] wakes up",
                        Result = "4"
                    }
                }
            };

            return testDataSets;
        }
    }
}
