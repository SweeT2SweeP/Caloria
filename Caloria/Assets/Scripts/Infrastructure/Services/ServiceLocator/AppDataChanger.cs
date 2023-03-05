using System;
using System.Collections.Generic;
using Infrastructure.Services.ServiceLocator.Data;
using UnityEngine;

namespace Infrastructure.Services.ServiceLocator
{
    public class AppDataChanger : IAppDataChanger
    {
        private const string DayDataKey = "DayData";
        private const string FoodDataKey = "FoodData";

        private IPrefs _prefs;

        public FoodDataCollection FoodData { get; private set; }

        public DayData CurrentDayData { get; private set; }

        public AppDataChanger(IPrefs prefs)
        {
            _prefs = prefs;
            
            LoadDayData();
            LoadFoodData();
        }

        public void AddNewFoodData(FoodData foodData)
        {
            FoodData.Data.Add(foodData);

            SaveFoodData();
        }

        public void ChangeTotalCaloriesValue(int newValue)
        {
            CurrentDayData.TotalCalories = newValue;
            
            SaveDayData();
        }

        public void ChangeExercisesCalories(int newValue)
        {
            CurrentDayData.ExerciesColories = newValue;
            
            SaveDayData();
        }

        public void ChangeStepsCalories(int newValue)
        {
            CurrentDayData.StepsCalories = newValue;
            
            SaveDayData();
        }

        private void LoadDayData()
        {
            var value = _prefs.LoadPref(DayDataKey);

            if (TryToSaveNewDayData(value)) return;

            CurrentDayData = JsonUtility.FromJson<DayData>(value);

            TryToResetDayData();
        }

        private void TryToResetDayData()
        {
            if (CurrentDayData.CurrentDay.Day == DateTime.Now.Day) return;
            
            CurrentDayData.CurrentDay = DateTime.Now;
            CurrentDayData.DayFoodData = new List<FoodDataCollection>();
            CurrentDayData.ExerciesColories = 0;
            CurrentDayData.StepsCalories = 0;
        }

        private bool TryToSaveNewDayData(string value)
        {
            if (!string.IsNullOrEmpty(value)) return false;
            
            CurrentDayData = new DayData
            {
                CurrentDay = DateTime.Now,
                TotalCalories = 1500,
                StepsCalories = 0,
                ExerciesColories = 0,
                DayFoodData = new List<FoodDataCollection>()
            };

            SaveDayData();

            return true;
        }

        private void LoadFoodData()
        {
            var value = _prefs.LoadPref(FoodDataKey);

            if (TryToInitFoodData(value)) return;

            FoodData = JsonUtility.FromJson<FoodDataCollection>(value);
        }

        private bool TryToInitFoodData(string value)
        {
            if (!string.IsNullOrEmpty(value)) return false;
            
            FoodData = new FoodDataCollection
            {
                Data = new List<FoodData>()
            };

            SaveFoodData();
            
            return true;
        }

        private void SaveFoodData() => 
            _prefs.SavePref(FoodDataKey, JsonUtility.ToJson(FoodData));
        
        private void SaveDayData() => 
            _prefs.SavePref(DayDataKey, JsonUtility.ToJson(CurrentDayData));
    }
}