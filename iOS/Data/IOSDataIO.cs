using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using System.Collections.Generic;
using MyFoodLog.Data;
using Foundation;

namespace AFoodLog.iOS.Data
{
    public class IOSDataIO : MyFoodLog.Services.IFoodDataIO
    {
        public IReadOnlyCollection<LogEntry> ReadLocalData()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(LogData));
            if (!File.Exists(FullDataPath))
            {
                return new List<LogEntry>();
            }

            try
            {
                using (var file = System.IO.File.OpenRead(FullDataPath))
                {
                    var data = serializer.Deserialize(file);
                    return ((LogData)data).LogEntries;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return new List<LogEntry>();
        }

        public void SaveLocalData(IReadOnlyCollection<LogEntry> logEntries)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(LogData));

            try
            {
                var data = new LogData() { LogEntries = logEntries.ToList() };
                using (var file = File.Create(FullDataPath))
                {
                    serializer.Serialize(file, data);
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private static string FullDataPath
        {
            get
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                return Path.Combine(path, fileName);
            }
        }


        #region Data

        private const string fileName = "logdata.xml";

        #endregion
    }

    [Preserve(AllMembers = true)]
    [Serializable]
    public class LogData
    {
        public LogData()
        {
        }

        public List<LogEntry> LogEntries { get => logEntries; set => logEntries = value; }

        private List<LogEntry> logEntries;
    }
}
