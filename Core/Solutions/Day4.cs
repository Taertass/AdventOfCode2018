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
            BeginsShift,
            FallsAsleep,
            WakesUp
        }
        
        public class GuardEvent
        {
            public int? GuardId { get; set; }

            public GuardEventType GuardEventType { get; set; }

            public DateTime TimeStamp { get; set; }
            
            public string Data { get; set; }

            public long TimeInEvent => NextGuardEvent != null ? (NextGuardEvent.TimeStamp.Ticks - TimeStamp.Ticks) : 0;

            public double MinutesInEvent => TimeSpan.FromTicks(TimeInEvent).TotalMinutes;

            public GuardEvent PreviousGuardEvent { get; set; }

            public GuardEvent NextGuardEvent { get; set; }
            

            public GuardEvent(string data) 
            {
                string datePart = data.Substring(1, 16);
                TimeStamp = DateTime.Parse(datePart);

                if (data.Contains("begins shift"))
                {
                    GuardEventType = GuardEventType.BeginsShift;
                }
                else if (data.Contains("falls asleep"))
                {
                    GuardEventType = GuardEventType.FallsAsleep;
                }
                else if (data.Contains("wakes up"))
                {
                    GuardEventType = GuardEventType.WakesUp;
                }

                var match = Regex.Match(data, "Guard #[0-9]+");
                if (match.Success)
                {
                    var splitIndex = match.Value.IndexOf('#');
                    var idText = match.Value.Substring(splitIndex + 1, match.Value.Length - splitIndex - 1);
                    GuardId = Convert.ToInt32(idText);
                }
            }
        }


        public class Guard
        {
            public int Id { get; set; }

            public List<GuardEvent> GuardEvents { get; set; } = new List<GuardEvent>();

            public void AddEvent(GuardEvent guardEvent)
            {
                GuardEvents.Add(guardEvent);
            }

            public bool IsAsleep(DateTime timestamp)
            {
                return false;
            }

            public double GetTotalSleepTime()
            {
                List<GuardEvent> fellAsleepEvents = GuardEvents.Where(g => g.GuardEventType == GuardEventType.FallsAsleep).ToList();
                var minutesAsleep = fellAsleepEvents.Sum(ge => ge.MinutesInEvent);

                return minutesAsleep;
            }

            public int GetMinuteMostAsleep()
            {
                List<GuardEvent> fellAsleepEvents = GuardEvents.Where(g => g.GuardEventType == GuardEventType.FallsAsleep).ToList();

                Dictionary<int, int> sumOfSleepMinutes = new Dictionary<int, int>();
                for(int i = 0; i < 60; i++)
                {
                    sumOfSleepMinutes.Add(i, 0);
                }

                for (int i = 0; i < 60; i++)
                {
                    int wasAsleepCount = fellAsleepEvents.Count(ge => ge.TimeStamp.Minute <= i && (i - ge.TimeStamp.Minute) < ge.MinutesInEvent);
                    if(wasAsleepCount > 0)
                        sumOfSleepMinutes[i] = wasAsleepCount;
                }


                var minutePair = sumOfSleepMinutes.OrderByDescending(v => v.Value).First();

                return minutePair.Key;
            }
        }

        public override string Solve1(string input)
        {            
            string[] lines = SplitByNewlineAsString(input);

            //Parse GuardEvents
            List<GuardEvent> guardEvents = new List<GuardEvent>();
            GuardEvent _previousGuardEvent = null;
            foreach (string line in lines)
            {
                GuardEvent guardEvent = new GuardEvent(line);

                if (_previousGuardEvent != null)
                {
                    guardEvent.PreviousGuardEvent = guardEvent;
                    _previousGuardEvent.NextGuardEvent = guardEvent;
                }

                _previousGuardEvent = guardEvent;

                guardEvents.Add(guardEvent);
            }
            
            //Parse Guard Events
            List<Guard> guards = new List<Guard>();
            Guard guard = null;
            foreach (GuardEvent guardEvent in guardEvents)
            {
                if(guardEvent.GuardId.HasValue)
                {
                    guard = guards.FirstOrDefault(g => g.Id == guardEvent.GuardId);
                    if(guard == null)
                    {
                        guard = new Guard()
                        {
                            Id = guardEvent.GuardId.Value,
                        };
                        guards.Add(guard);
                    }
                }
                guard.AddEvent(guardEvent);
            }

            //Get guard with most time asleep
            Guard guardMostAsleep = guards.OrderByDescending(g => g.GetTotalSleepTime()).First();
            double timeAsleep = guardMostAsleep.GetTotalSleepTime();

            //Get minute of day that he is most asleep
            double minuteMostAsleep = guardMostAsleep.GetMinuteMostAsleep();

            return (guardMostAsleep.Id * minuteMostAsleep).ToString();
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
                        Result = "240"
                    }
                }
            };

            return testDataSets;
        }
    }
}
