using System;
using System.Collections.Generic;

namespace MyFoodLog.Services
{
    public class FLServiceProvider
    {
        private static void TryInitialize()
        {
            if (Instance == null)
                Instance = new FLServiceProvider();
        }

        private FLServiceProvider()
        {
            //Services = new Dictionary<Type, object>();

            //var localLog = new LocalLog(new IOSDataIO());
            //Services.Add(typeof(IFLLog), localLog);
            //Services.Add(typeof(ILogEntryCreator), localLog);
        }

        public static T Get<T>()
            where T : FLService
        {
            TryInitialize();

            return (T)Instance.Services[typeof(T)];
        }

        private Dictionary<Type, object> Services;
        private static FLServiceProvider Instance;
    }
}
