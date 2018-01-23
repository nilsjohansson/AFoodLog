using System;
namespace MyFoodLog.Data
{
    public class LogEntryOccurrence
    {
        public LogEntryOccurrence(string name, int occurrences)
        {
            Name = name;
            Occurrences = occurrences;
        }

        public string Name { get; }
        public int Occurrences { get; }
    }
}
