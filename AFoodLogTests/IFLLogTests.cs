using System;
using System.Linq;
using NUnit.Framework;
using MyFoodLog.Services;

namespace FoodLogTests
{
    [TestFixture()]
    public class IFLLogTests
    {
        private string[] Food = new string[] { "A cheescake", "Bread", "Hamburger", "Salad" };

        public IFLLogTests()
        {
            TestAssistant.ClearLog();
        }

        [TearDown]
        public void CleanUp()
        {
            TestAssistant.ClearLog();
        }

        [Test()]
        public void AddEntries()
        {
            Console.WriteLine("AddEntries test.");
            var log = FLServiceProvider.Get<IFLLog>();

            var entry = log.CreateNew("Abc");
            var logEntries = log.Read(DateTime.Now - TimeSpan.FromDays(100), DateTime.Now).Count();

            Assert.AreEqual(1, logEntries, "There should be items stored in the log.");
        }

        [Test()]
        public void RemoveEntries()
        {
            Console.WriteLine("RemoveEntries test.");
            var log = FLServiceProvider.Get<IFLLog>();

            var entry = log.CreateNew("Abc");

            log.Remove(entry);

            int logEntries = log.Read(DateTime.Now - TimeSpan.FromDays(100), DateTime.Now).Count();
            Assert.AreEqual(0, logEntries, "The log should be empty, Remove entry failed.");
        }

        [Test()]
        public void CanAddAndReadEntries()
        {
            Console.WriteLine("CanAddAndReadEntries test.");
            var log = FLServiceProvider.Get<IFLLog>();

            var beginning = DateTime.Now;

            log.CreateNew(Food[0]);
            System.Threading.Thread.Sleep(250);

            var theFirstTime = DateTime.Now;

            log.CreateNew(Food[1]);
            System.Threading.Thread.Sleep(250);

            var theSecondTime = DateTime.Now;

            log.CreateNew(Food[2]);

            int count = log.Read(beginning, DateTime.Now).Count(x => Food.Any(y => y == x.Name));
            Assert.AreEqual(3, count, "The log should contain 3 entries.");

            count = log.Read(beginning, theSecondTime).Count(x => Food.Any(y => y == x.Name));
            Assert.AreEqual(2, count, "The log should contain 2 entries.");

            count = log.Read(theSecondTime, DateTime.Now).Count(x => Food.Any(y => y == x.Name));
            Assert.AreEqual(1, count, "The log should contain 1 entries.");
        }

        [Test()]
        public void EntriesAreReadInCorrectOrder()
        {
            Console.WriteLine("EntriesAreReadInCorrectOrder test.");
            var log = FLServiceProvider.Get<IFLLog>();

            var beginning = DateTime.Now;

            log.CreateNew(Food[0]);
            System.Threading.Thread.Sleep(50);

            log.CreateNew(Food[1]);
            System.Threading.Thread.Sleep(50);

            log.CreateNew(Food[2]);
            System.Threading.Thread.Sleep(50);

            log.CreateNew(Food[3]);

            var output = log.Read(beginning, DateTime.Now);

            for (int i = 1; i < 3; i++)
            {
                var prev = output.ToArray()[i - 1];
                var curr = output.ToArray()[i];
                Assert.IsTrue(curr.TimeStamp > prev.TimeStamp, $"Item {i} was in wrong order.current item: {curr.TimeStamp}, prev: {prev.TimeStamp}");
            }
        }
    }
}