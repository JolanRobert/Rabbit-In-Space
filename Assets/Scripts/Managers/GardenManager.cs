using System;
using System.Collections.Generic;
using UnityEngine;

public class GardenManager : MonoBehaviour {

    public static GardenManager Instance;

    [Header("Food Shop")]
    public List<FoodSO> foodList;
    [SerializeField] private GameObject fs_entryPrefab;
    [SerializeField] private Transform fs_contentParent;

    [Header("ParcelUpgrades")]
    public List<ParcelUpgradeSO> upgradeList;
    [SerializeField] private GameObject pu_entryPrefab;
    [SerializeField] private Transform pu_contentParent;
    
    [Header("Current Selection")]
    public Parcel myParcel;
    public int mySlot;

    void Awake() {
        Instance = this;
    }

    void Start() {
        foreach (FoodSO foodSo in foodList) {
            GameObject entry = Instantiate(fs_entryPrefab, fs_contentParent);
            entry.GetComponent<FoodShopEntry>().Init(foodSo);
        }
        
        foreach (ParcelUpgradeSO puSo in upgradeList) {
            GameObject entry = Instantiate(pu_entryPrefab, pu_contentParent);
            entry.GetComponent<ParcelUpgradeEntry>().Init(puSo);
            UIGarden.Instance.upgrades.Add(entry.GetComponent<ParcelUpgradeEntry>());
        }
    }
    
    public void SelectParcel(Parcel parcel) {
        myParcel = parcel;
    }

    public void SelectSlot(int foodSlot) {
        mySlot = foodSlot;
    }

    public void PlantSeed(GetEnum myEnum) {
        FoodSO foodSO = GetCorrectFoodSo(myEnum.itemType);
        myParcel.foodList[mySlot].StartNewFood(foodSO);
        UIGarden.Instance.CloseMenuSeed();
    }

    private FoodSO GetCorrectFoodSo(ItemType itemType) {
        foreach (FoodSO fso in foodList) {
            if (fso.itemType == itemType) return fso;
        }

        throw new Exception("Unknown Food");
    }
}
