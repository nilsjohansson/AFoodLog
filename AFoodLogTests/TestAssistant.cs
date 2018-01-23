using System;
using MyFoodLog.Services;

namespace FoodLogTests
{
    public class TestAssistant
    {
        public TestAssistant()
        {
        }

        public static void ClearLog()
        {
            var localLog = new LocalLog();
            localLog.Clear();
        }
    }
}
