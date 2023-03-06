using TMPro;
using UnityEngine;

namespace UI
{
    public class FoodElement : MonoBehaviour
    {
        [SerializeField] private TMP_Text _foodName;
        [SerializeField] private TMP_Text _caloriesCount;

        public void Init(string foodName, int caloriesCount)
        {
            _foodName.text = foodName;
            _caloriesCount.text = caloriesCount.ToString();
        }
    }
}
