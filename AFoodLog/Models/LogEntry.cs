using System;

namespace MyFoodLog.Data
{
    public class LogEntry
    {
        public LogEntry()
        {

        }

        public LogEntry(string name, DateTime timeStamp, NutritionData nutrition = null)
        {
            Name = name;
            TimeStamp = timeStamp;
            Nutrition = nutrition ?? new NutritionData();
			Image = new byte[0];
            Id = Guid.NewGuid();
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
            }
        }

        public DateTime TimeStamp
        {
            get => timeStamp;

            set
            {
                timeStamp = value;
            }
        }

        public byte[] Image
        {
            get => image;

            set
            {
                image = value;
            }
        }

        public NutritionData Nutrition
        {
            get => nutrition;

            set
            {
                nutrition = value;
            }
        }

        public Guid Id
        {
            get
            {
                return id;
            }

            private set
            {
                id = value;
            }
        }

        #region Data

		Guid id;
        NutritionData nutrition;
        string name;
        DateTime timeStamp;
        byte[] image;

		#endregion
    }
}