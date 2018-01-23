using System;
using System.Collections.Generic;
using System.Linq;
using MyFoodLog.Data;
using MyFoodLog.Data.Extensions;

namespace MyFoodLog.Services
{
    public class LocalLog : IFLLog, ILogEntryCreator
    {
        private readonly IFoodDataIO _foodDataIO;

        public LocalLog(IFoodDataIO foodDataIO)
        {
            _foodDataIO = foodDataIO;
        }

        public bool Add(string logEntryName)
        {
            return CreateNew(logEntryName) != null;
        }

        public LogEntry CreateNew(string logEntryName)
        {
            logEntries = _foodDataIO.ReadLocalData().ToList();

            var entry = new LogEntry(logEntryName, DateTime.Now);
            logEntries.Add(entry);

            _foodDataIO.SaveLocalData(logEntries);

            return entry;
        }

        public IReadOnlyCollection<LogEntryOccurrence> FindPrevious(string partialName)
        {
            logEntries = _foodDataIO.ReadLocalData().ToList();

            var matchingPreviousEntries = logEntries.Where(x => x.PartOfNameIs(partialName)).ToList();
            var distinctEntries = GetDistinctByName(matchingPreviousEntries);

            var result = new List<LogEntryOccurrence>();
            foreach (var match in distinctEntries)
            {
                var occurrences = matchingPreviousEntries.Count(mpo => match.HasSameNameAs(mpo));
                result.Add(new LogEntryOccurrence(match.Name, occurrences));
                Console.WriteLine($"Match: {match.Name}: {occurrences}x");
            }

            return result;
        }

        private IEnumerable<LogEntry> GetDistinctByName(List<LogEntry> matchingPreviousEntries)
        {
            var result = new List<LogEntry>();

            foreach(var item in matchingPreviousEntries)
            {
                if (!result.Any(x => x.HasSameNameAs(item)))
                    result.Add(item);
            }

            return result;
        }

        public IReadOnlyCollection<LogEntry> Read(DateTime from, DateTime to)
        {
            logEntries = _foodDataIO.ReadLocalData().ToList();

            return logEntries.Where(x => x.TimeStamp >= from && x.TimeStamp <= to).ToList();
        }

        public bool Remove(LogEntry logEntry)
        {
            logEntries = _foodDataIO.ReadLocalData().ToList();

            try
            {
                logEntries.Remove(logEntries.Single(x => x.Id == logEntry.Id));
                _foodDataIO.SaveLocalData(logEntries);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        public void Clear()
        {
            logEntries = new List<LogEntry>();
            _foodDataIO.SaveLocalData(logEntries);
        }

        private List<LogEntry> logEntries;
    }
}
