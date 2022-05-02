using System.Collections.Generic;
using UnityEngine;

public class GardenManager : MonoBehaviour {

    public static GardenManager Instance;

    [Header("Current Selection")]
    public Parcel myParcel;
    public int mySlot;
    
    //Parcel ID, Upgrade List (ParcelUpgrade Memory)
    public Dictionary<int, List<Parcel.Upgrade>> upgrades = new Dictionary<int, List<Parcel.Upgrade>>();

    void Awake() {
        if (Instance != null) Destroy(gameObject);
        else Instance = this;
    }

    void Start() {
        for (int i = 0; i < 6; i++) {
            upgrades.Add(i,new List<Parcel.Upgrade>());
            foreach (ParcelUpgradeSO puSo in DataManager.Instance.parcelUpgradeList) {
                upgrades[i].Add(new Parcel.Upgrade(puSo.upgradeType));
            }
        }
    }
    
    //Plant with seed menu
    public void PlantSeed(FoodType itemType) {
        foreach (FoodSO foodSo in DataManager.Instance.foodList) {
            if (foodSo.foodType != itemType) continue;
            myParcel.foodList[mySlot].StartNewFood(foodSo);
            GrowRice.OnPlantMoonRice.Invoke();
            UIGarden.Instance.CloseMenuSeed();
        }
    }
}
