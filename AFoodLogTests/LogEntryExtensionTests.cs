using System;
using System.Linq;
using NUnit.Framework;
using MyFoodLog.Services;
using MyFoodLog.Data;
using MyFoodLog.Data.Extensions;
using System.Collections.Generic;

namespace FoodLogTests
{
    [TestFixture()]
    public class LogEntryExtensionTests
    {
        private string[] Food = new string[] { "A cheescake", "Bread", "Hamburger", "Salad" };

        public LogEntryExtensionTests()
        {
        }

        [Test()]
        [TestCase(true, "Abc", "Abc")]
        [TestCase(true, "abc", "ABC")]
        [TestCase(false, "abc", "bBC")]
        public void LogEntryNamesCompareRegardlessOfCasing(bool expected, string name1, string name2)
        {
            var a = new LogEntry(name1, DateTime.Now);
            var b = new LogEntry(name2, DateTime.Now);

            var outcome = (new LogEntryNameComparer()).Equals(a, b);

            Assert.AreEqual(expected, outcome, $"Incorrect comparison. Outcome was {name1} & {name2} == {outcome}");
        }

        [Test()]
        [TestCase(true, "Abc", "Abc")]
        [TestCase(true, "abc", "ABC")]
        [TestCase(false, "abc", "bBC")]
        [TestCase(true, "A cheescake", "a Cheescake")]
        [TestCase(true, "A cheescake", "a cheescake")]
        [TestCase(true, "Bread", "bread")]
        public void LogEntryNamesAreSameExtension(bool expected, string name1, string name2)
        {
            Console.WriteLine("LogEntryNamesAreSameExtension test.");
            var a = new LogEntry(name1, DateTime.Now);
            var b = new LogEntry(name2, DateTime.Now);

            var outcome = a.HasSameNameAs(b);

            Assert.AreEqual(expected, outcome, $"Incorrect comparison. Outcome was {name1} & {name2} == {outcome}");
        }

        [Test()]
        [TestCase(false, "abc", "bBC")]
        [TestCase(true, "abc", "abcdefgh")]
        [TestCase(false, "abc", "aabcdefgh")]
        [TestCase(false, "abc", "somethingelse")]
        public void LogEntryNameSubstring(bool expected, string subString, string name)
        {
            var a = new LogEntry(name, DateTime.Now);

            var outcome = a.PartOfNameIs(subString);

            Assert.AreEqual(expected, outcome, $"Incorrect substring check. Code thinks '{subString}' is substring of '{name}' is {outcome}");
        }


    }
}
