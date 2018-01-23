using System;
using System.Linq;
using NUnit.Framework;
using MyFoodLog.Services;

namespace FoodLogTests
{
    [TestFixture()]
    public class ILogEntryCreatorTests
    {
        private string[] Food = new string[] { "A cheescake", "Bread", "Hamburger", "Salad", "Bangers and mash" };
        public ILogEntryCreatorTests()
        {
            TestAssistant.ClearLog();
        }

        [TearDown]
        public void CleanUp()
        {
            TestAssistant.ClearLog();   
        }

        [Test()]
        [TestCase(1, "A cheescake")]
        [TestCase(0, "ZZZ")]
        [TestCase(2, "B")]
        [TestCase(1, "a CHEEscake")]
        [TestCase(0, "zzz")]
        [TestCase(2, "b")]
        public void PreviousEntriesStringMatching(int expected, string partialString)
        {
            Console.WriteLine($"PreviousEntriesStringMatching test. {expected}, '{partialString}'");
            var log = FLServiceProvider.Get<ILogEntryCreator>();

            foreach (var food in Food)
                log.Add(food);

            var previousSearches = log.FindPrevious(partialString);
            Assert.AreEqual(expected, previousSearches.Count(), $"Partial string '{partialString}' gave an unexpected number of results.");
        }

        [Test()]
        [TestCase(2, "A cheescake")]
        [TestCase(3, "B")]
        [TestCase(1, "Ba")]
        [TestCase(2, "Br")]
        public void TotalExpectedOccurencesCount(int expected, string partialString)
        {
            Console.WriteLine($"TotalExpectedOccurencesCount test. {expected}, '{partialString}'");
            var log = FLServiceProvider.Get<ILogEntryCreator>();

            foreach (var food in Food)
                log.Add(food);
            
            log.Add(Food[0]);
            log.Add(Food[1]);
            log.Add(Food[2]);

            var previousSearches = log.FindPrevious(partialString);
            Assert.AreEqual(expected, previousSearches.Sum(x => x.Occurrences), $"Partial string '{partialString}' gave an unexpected number of results.");
        }
    }
}