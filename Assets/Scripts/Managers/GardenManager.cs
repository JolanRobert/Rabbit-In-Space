using System;
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

    public void PlantSeed(GetEnum ge) {
        FoodSO foodSO = GetCorrectFoodSo(ge.itemType);
        myParcel.foodSlots[mySlot].InitFood(foodSO);
        UIManager.Instance.GetGardenUI().CloseMenuSeed();
    }

    private FoodSO GetCorrectFoodSo(ItemType itemType) {
        foreach (FoodSO fso in foodList) {
            if (fso.itemType == itemType) return fso;
        }

        throw new Exception("Unknown Food");
    }
}

public enum ItemType {
    NONE, MOON_RICE, STARBERRY, NEBULAZUKI
}
