using System.Collections.Generic;
using UnityEngine;

public class GardenManager : MonoBehaviour {

    public static GardenManager Instance;

    [Header("Current Selection")]
    public Parcel myParcel;
    public int mySlot;

    public List<Parcel> parcelList;
    
    //Parcel ID, Upgrade List (ParcelUpgrade Memory)
    //public Dictionary<int, List<Parcel.Upgrade>> upgrades = new Dictionary<int, List<Parcel.Upgrade>>();

    void Awake() {
        if (Instance != null) Destroy(gameObject);
        else Instance = this;
    }

    public void SelectParcel(GardenEntry entry) {
        myParcel = parcelList[entry.transform.GetSiblingIndex()];
        myParcel.gardenEntry = entry;
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
