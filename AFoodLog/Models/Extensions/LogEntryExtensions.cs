using System;
using System.Collections.Generic;

namespace MyFoodLog.Data.Extensions
{
    public static class LogEntryExtensions
    {
        public static bool PartOfNameIs(this LogEntry source, string partialName)
        {
            return source.Name.ToLower().StartsWith(partialName.ToLower(), StringComparison.InvariantCulture);
        }

        public static bool HasSameNameAs(this LogEntry source, LogEntry other)
        {
            string sourceName = source.Name.ToLower();
            string otherName = other.Name.ToLower();

            Console.WriteLine($"HasSameNameAs {sourceName} : {otherName} == {sourceName == otherName}");

            return sourceName == otherName;
        }
    }
}
