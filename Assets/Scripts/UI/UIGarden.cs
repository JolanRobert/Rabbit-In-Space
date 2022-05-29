using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UIGarden : MonoBehaviour {

    public static UIGarden Instance;

    [Header("Parcel Menu")]
    [SerializeField] private ParcelMenu parcelMenu;
    public List<ParcelMenuEntry> plants;
    
    [Header("Parcel Upgrade")]
    public List<ParcelUpgradeEntry> upgrades; //Contains the list of upgrades to display when ParcelMenuUpgrade is open
    
    [Header("Food Shop")]
    [SerializeField] private GameObject foodShopContent;
    [SerializeField] private GameObject closeOverlay;

    [Header("Garden Menu")]
    [SerializeField] private GameObject gardenMenu;
    [SerializeField] private List<GardenEntry> gardenEntries;

    void Awake() {
        Instance = this;
    }

    public void OpenParcelMenu() {
        Parcel myParcel = GardenManager.Instance.myParcel;
        
        SetupUpgrades();
        SetupParcelMenu(myParcel);
        SetupParcelUpgrade(myParcel);
        
        UIManager.Instance.OpenPanel(parcelMenu.gameObject);
    }

    public void CloseParcelMenu() {
        GardenManager.Instance.myParcel.gardenEntry.CloseParcel();
        foreach (Food food in GardenManager.Instance.myParcel.foodList) {
            food.foodUI = null;
        }
        GardenManager.Instance.myParcel = null;
        UIManager.Instance.ClosePanel(parcelMenu.gameObject);
    }

    public void SetupUpgrades() {
        parcelMenu.SetupUpgrades(GardenManager.Instance.myParcel);
    }

    private void SetupParcelMenu(Parcel parcel) {
        for (int i = 0; i < plants.Count; i++) {
            parcel.foodList[i].foodUI = plants[i];
            parcel.foodList[i].InitFoodUI();
        }
    }

    private void SetupParcelUpgrade(Parcel parcel) {
        foreach (UpgradeType item in Enum.GetValues(typeof(UpgradeType))) {
            bool isBought = parcel.IsUpgradeBought(item);
            bool isActive = parcel.IsUpgradeActive(item);
            
            upgrades[(int)item].SetupUpgrade(isBought,isActive);
            
            if (item == UpgradeType.GRAINATOR) {
                for (int i = 0; i < parcel.foodList.Length; i++) {
                    plants[i].ShowGrainator(isActive);
                    parcel.foodList[i].SetGrainatorFood(isActive ? parcel.foodList[i].grainatorFood : FoodType.NONE);
                }
            }
        }
    }
    
    public void OpenMenuSeed(int foodSlot) {
        foodShopContent.transform.DOComplete();
        parcelMenu.transform.DOComplete();
        
        foodShopContent.SetActive(true);
        closeOverlay.SetActive(true);
        foodShopContent.transform.DOMoveX(foodShopContent.transform.position.x - 300, 0.325f);
        parcelMenu.transform.DOMoveX(parcelMenu.transform.position.x - 150, 0.325f);
        GardenManager.Instance.mySlot = foodSlot;
    }

    public void CloseMenuSeed() {
        foodShopContent.transform.DOComplete();
        parcelMenu.transform.DOComplete();
        
        closeOverlay.SetActive(false);
        foodShopContent.transform.DOMoveX(foodShopContent.transform.position.x + 300, 0.325f);
        parcelMenu.transform.DOMoveX(parcelMenu.transform.position.x + 150, 0.325f).OnComplete(() => {
            foodShopContent.SetActive(false);
        });
    }

    public void OpenGarden() {
        if (KitchenManager.Instance.inService) return;
        UIManager.Instance.OpenPanel(gardenMenu);
    }
}
