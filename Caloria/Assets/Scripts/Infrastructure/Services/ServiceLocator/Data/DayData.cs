using System;
using System.Collections.Generic;
using System.Globalization;

namespace Infrastructure.Services.ServiceLocator.Data
{
    [Serializable]
    public class DayData
    {
        public string CurrentDayStringValue;
        public int TotalCalories;
        public int StepsCalories;
        public int ExerciesColories;
        public EatenFoodCollection DayFoodData;

        public DateTime CurrentDay
        {
            get => DateTime.Parse(CurrentDayStringValue);
            set => CurrentDayStringValue = value.ToString(CultureInfo.InvariantCulture);
        }
    }
}