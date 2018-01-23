using System;
namespace MyFoodLog.Data
{
    public class NutritionData
    {
        public NutritionData()
        {
        }

        public int KCal
        {
            get
            {
                return kCal;
            }

            set
            {
                kCal = value;
            }
        }

        public int Protein
        {
            get
            {
                return protein;
            }

            set
            {
                protein = value;
            }
        }

        public int Fat
        {
            get
            {
                return fat;
            }

            set
            {
                fat = value;
            }
        }

        public int Carbohydrates
        {
            get
            {
                return carbohydrates;
            }

            set
            {
                carbohydrates = value;
            }
        }

        #region Data

        private int kCal;
        private int protein;
        private int fat;
        private int carbohydrates;

        #endregion
    }
}
