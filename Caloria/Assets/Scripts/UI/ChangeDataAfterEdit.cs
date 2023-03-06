using System.Linq;
using Infrastructure.Services.ServiceLocator;
using Infrastructure.Services.ServiceLocator.Data;
using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(TMP_InputField))]
    public class ChangeDataAfterEdit : MonoBehaviour
    {
        [SerializeField] private ActionType actionType;
        
        private TMP_InputField _inputField;

        private IAppDataChanger _appDataChanger;

        private void Awake()
        {
            _inputField = GetComponent<TMP_InputField>();
            _appDataChanger = AllServices.Container.Single<IAppDataChanger>();
            _appDataChanger.DataUpdated += UpdateField;
            
            UpdateField();

            _inputField.onEndEdit.AddListener(delegate { EditData(); });
        }

        private void EditData()
        {
            var value = int.Parse(_inputField.text);

            switch (actionType)
            {
                case ActionType.ChangeTotalCaloriesValue:
                    _appDataChanger.ChangeTotalCaloriesValue(value);
                    break;
                case ActionType.ChangeStepsValue:
                    _appDataChanger.ChangeStepsCalories(value);
                    break;
                case ActionType.ChangeExercisesCaloriesValue:
                    _appDataChanger.ChangeExercisesCalories(value);
                    break;
                default:
                    Debug.Log("Nothing happened");
                    break;
            }
        }

        private void UpdateField()
        {
            switch (actionType)
            {
                case ActionType.ChangeTotalCaloriesValue:
                    UpdateTotalCaloriesInputField();
                    break;
                case ActionType.ChangeStepsValue:
                    UpdateStepsInputField();
                    break;
                case ActionType.ChangeExercisesCaloriesValue:
                    UpdateExercisesInputField();
                    break;
                default:
                    Debug.Log("Nothing Happened");
                    break;
            }
        }

        private void UpdateStepsInputField() => 
            _inputField.text = _appDataChanger.CurrentDayData.StepsCalories.ToString();

        private void UpdateExercisesInputField() => 
            _inputField.text = _appDataChanger.CurrentDayData.ExerciesColories.ToString();

        private void UpdateTotalCaloriesInputField()
        {
            var sumOfCalories = _appDataChanger.CurrentDayData.DayFoodData.Data
                .Select(eatenFood => new
                {
                    eatenFood,
                    foodData = _appDataChanger.FoodData.Data
                        .FirstOrDefault(s => s.FoodName == eatenFood.FoodName)
                })
                .Where(t => t.foodData != null)
                .Select(t => t.eatenFood.FoodWeight * t.foodData.FoodCalories / 100)
                .Sum();

            var value = _appDataChanger.CurrentDayData.TotalCalories
                        + _appDataChanger.CurrentDayData.StepsCalories
                        + _appDataChanger.CurrentDayData.ExerciesColories
                        - sumOfCalories;

            _inputField.text = value < _appDataChanger.CurrentDayData.TotalCalories
                ? value.ToString()
                : _appDataChanger.CurrentDayData.TotalCalories.ToString();
        }
    }
}
