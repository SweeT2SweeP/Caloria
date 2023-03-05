using System;
using System.Collections.Generic;

namespace Infrastructure.Services.ServiceLocator.Data
{
    [Serializable]
    public class DayData
    {
        public DateTime CurrentDay;
        public int TotalCalories;
        public int StepsCalories;
        public int ExerciesColories;
        public List<FoodDataCollection> DayFoodData;
    }
}