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
    
    //Plant with seed menu
    public void PlantSeed(FoodType itemType) {
        foreach (FoodSO foodSo in foodList) {
            if (foodSo.foodType != itemType) continue;
            myParcel.foodList[mySlot].StartNewFood(foodSo);
            UIGarden.Instance.CloseMenuSeed();
        }
    }
}
