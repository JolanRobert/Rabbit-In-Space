using UnityEngine;

public class GardenManager : MonoBehaviour {

    public static GardenManager Instance;

    [Header("Current Selection")]
    public Parcel myParcel;
    public int mySlot;

    void Awake() {
        Instance = this;
    }
    
    //Plant with seed menu
    public void PlantSeed(FoodType itemType) {
        foreach (FoodSO foodSo in DataManager.Instance.foodList) {
            if (foodSo.foodType != itemType) continue;
            myParcel.foodList[mySlot].StartNewFood(foodSo);
            UIGarden.Instance.CloseMenuSeed();
        }
    }
}
