using UnityEngine;

public class GardenManager : MonoBehaviour {

    public static GardenManager Instance;

    [SerializeField] private GameObject gardenPanel;
    
    [Header("Current Selection")]
    public Parcel myParcel;
    public int mySlot;

    void Awake() {
        Instance = this;
    }

    public void OpenGarden() {
        UIManager.Instance.OpenPanel(gardenPanel);
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
