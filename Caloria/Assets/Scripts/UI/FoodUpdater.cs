using System;
using System.Linq;
using Infrastructure.Services.ServiceLocator;
using Infrastructure.Services.ServiceLocator.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FoodUpdater : MonoBehaviour
    {
        [Header("Main window")] 
        [SerializeField] private GameObject _foodElement;
        [SerializeField] private Transform _foodListContainer;
        [SerializeField] private TMP_InputField _foodInputField;
        [SerializeField] private TMP_InputField _weightInputField;
        [SerializeField] private Button _addFoodButton;

        [Header("Add new food form")] 
        [SerializeField] private GameObject _newFoodForm;
        [SerializeField] private TMP_InputField _newFoodInputField;
        [SerializeField] private TMP_InputField _newCaloriesCountInputField;
        [SerializeField] private Button _addNewFoodButton;
        [SerializeField] private Button _closeForm;

        private IAppDataChanger _appDataChanger;

        private void Awake()
        {
            _appDataChanger = AllServices.Container.Single<IAppDataChanger>();
            _appDataChanger.DataUpdated += InitFoodList;
            
            InitFoodList();
            InitAddFoodButton();
            InitAddNewFoodButton();
            InitCloseButton();
        }

        private void InitFoodList()
        {
            foreach (Transform foodElement in _foodListContainer) 
                Destroy(foodElement.gameObject);

            foreach (var eatenFood in _appDataChanger.CurrentDayData.DayFoodData.Data)
            {
                var element = Instantiate(_foodElement, _foodListContainer);
                var foodData = _appDataChanger.FoodData.Data
                    .FirstOrDefault(s => s.FoodName == eatenFood.FoodName);
                
                if (foodData == null) continue;
                
                var calories = eatenFood.FoodWeight * foodData.FoodCalories / 100; 
                
                element.GetComponent<FoodElement>().Init(eatenFood.FoodName, calories);
            }
        }

        private void InitAddFoodButton()
        {
            _addFoodButton.onClick.AddListener(() =>
            {
                if (string.IsNullOrEmpty(_foodInputField.text) || string.IsNullOrEmpty(_weightInputField.text))
                    return;

                if (_appDataChanger.FoodData.Data.Any(s => s.FoodName == _foodInputField.text))
                {
                    _appDataChanger.AddEatenFood(new EatenFood()
                    {
                        FoodName = _foodInputField.text,
                        FoodWeight = int.Parse(_weightInputField.text)
                    });

                    _foodInputField.text = "";
                    _weightInputField.text = "";
                }
                else
                {
                    _newFoodForm.SetActive(true);
                    _newFoodInputField.text = _foodInputField.text;
                }
            });
        }

        private void InitAddNewFoodButton()
        {
            _addNewFoodButton.onClick.AddListener(() =>
            {
                if (string.IsNullOrEmpty(_newFoodInputField.text) ||
                    string.IsNullOrEmpty(_newCaloriesCountInputField.text))
                    return;

                _appDataChanger.AddNewFoodData(new FoodData
                {
                    FoodName = _newFoodInputField.text,
                    FoodCalories = int.Parse(_newCaloriesCountInputField.text)
                });
                
                _newFoodInputField.text = "";
                _newCaloriesCountInputField.text = "";
                _newFoodForm.SetActive(false);
                
                if (_appDataChanger.FoodData.Data.Any(s => s.FoodName == _foodInputField.text))
                {
                    _appDataChanger.AddEatenFood(new EatenFood()
                    {
                        FoodName = _foodInputField.text,
                        FoodWeight = int.Parse(_weightInputField.text)
                    });

                    _foodInputField.text = "";
                    _weightInputField.text = "";
                }
            });
        }

        private void InitCloseButton()
        {
            _closeForm.onClick.AddListener(() =>
            {
                _newFoodForm.SetActive(false);
                _newFoodInputField.text = "";
                _newCaloriesCountInputField.text = "";
            });
        }
    }
}
