using System.Collections.Generic;
using UnityEngine;

public class GardenManager : MonoBehaviour {

    public static GardenManager Instance;

    [SerializeField] private List<FoodSO> foodList;
    
    public Parcel myParcel;
    public int mySlot;

    void Awake() {
        Instance = this;
    }
    
    public void SelectParcel(Parcel parcel) {
        myParcel = parcel;
    }

    public void SelectFood(int foodSlot) {
        mySlot = foodSlot;
    }

    public void PlantSeed(int seedIndex) {
        myParcel.foodSlots[mySlot].InitFood(foodList[seedIndex]);
        UIManager.Instance.GetGardenUI().CloseMenuSeed();
    }
}
