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

    public void PlantSeed(EnumManager myEnum) {
        FoodSO foodSO = GetCorrectFoodSo(myEnum.itemType);
        myParcel.foodList[mySlot].InitFood(foodSO);
        UIGarden.Instance.CloseMenuSeed();
    }

    private FoodSO GetCorrectFoodSo(ItemType itemType) {
        foreach (FoodSO fso in foodList) {
            if (fso.itemType == itemType) return fso;
        }
        throw new Exception("Unknown Food");
    }
}
