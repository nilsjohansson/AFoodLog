using System;
using System.Collections.Generic;
using MyFoodLog.Data;

namespace MyFoodLog.Services
{
    public interface IFLLog : FLService
    {
        LogEntry CreateNew(string logEntryName);
        bool Remove(LogEntry entry);
        IReadOnlyCollection<LogEntry> Read(DateTime from, DateTime to);
        void Clear();
    }
}