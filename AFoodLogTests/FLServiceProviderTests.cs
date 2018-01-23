using System;
using NUnit.Framework;
using MyFoodLog.Services;

namespace FoodLogTests
{
    [TestFixture()]
    public class FLServiceProviderTests
    {
        [Test()]
        public void ShouldGetAnExistingService()
        {
            var logInstance = FLServiceProvider.Get<IFLLog>();

            Assert.IsNotNull(logInstance);

            var logCreatorInstance = FLServiceProvider.Get<ILogEntryCreator>();

            Assert.IsNotNull(logCreatorInstance);
        }

        [Test()]
        public void ShouldNotGetInvalidServices()
        {
            Assert.Throws<System.Collections.Generic.KeyNotFoundException>(() => FLServiceProvider.Get<NonExistantService>());
        }
    }

    internal interface NonExistantService : FLService
    {
        
    }
}
