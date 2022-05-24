using UnityEngine;

public class DebugManager : MonoBehaviour {
    
    void Start() {
        foreach (FoodSO food in DataManager.Instance.foodList) {
            FoodDataManager.Instance.SetItem(food.foodType,100);
        }
    }
}
