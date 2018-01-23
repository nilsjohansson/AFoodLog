using System.Collections.Generic;
using MyFoodLog.Data;

namespace MyFoodLog.Services
{
    public interface ILogEntryCreator : FLService
    {
        bool Add(string logEntryName);
        IReadOnlyCollection<LogEntryOccurrence> FindPrevious(string partialName);
    }
}