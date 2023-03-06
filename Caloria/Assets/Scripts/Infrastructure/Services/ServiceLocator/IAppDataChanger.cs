using System;
using Infrastructure.Services.ServiceLocator.Data;

namespace Infrastructure.Services.ServiceLocator
{
    public interface IAppDataChanger : IService
    {
        event Action DataUpdated;
        FoodDataCollection FoodData { get; }
        DayData CurrentDayData { get; }
        void AddNewFoodData(FoodData foodData);
        void ChangeTotalCaloriesValue(int newValue);
        void ChangeExercisesCalories(int newValue);
        void ChangeStepsCalories(int newValue);
    }
}