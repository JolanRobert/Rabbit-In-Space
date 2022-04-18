using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UIGarden : MonoBehaviour {

    public static UIGarden Instance;

    [Header("Parcel Menu")]
    [SerializeField] private GameObject parcelMenu;
    public List<ParcelMenuEntry> plants;
    
    [Header("Parcel Upgrade")]
    public List<ParcelUpgradeEntry> upgrades; //Contains the list of upgrades to display when ParcelMenuUpgrade is open
    
    [Header("Food Shop")]
    [SerializeField] private GameObject foodShop;
    [SerializeField] private GameObject closeOverlay;

    void Awake() {
        Instance = this;
    }

    public void OpenParcelMenu() {
        Parcel myParcel = GardenManager.Instance.myParcel;
        
        //Get Parcel values into ParcelMenu UI Menu
        for (int i = 0; i < plants.Count; i++) {
            myParcel.foodList[i].foodUI = plants[i];
            myParcel.foodList[i].InitFoodUI();
        }
        
        //Setup ParcelMenuUpgrade with Parcel values
        foreach (UpgradeType item in Enum.GetValues(typeof(UpgradeType))) {
            bool isBought = myParcel.IsUpgradeBought(item);
            bool isActive = myParcel.IsUpgradeActive(item);
            
            upgrades[(int)item].SetupUpgrade(isBought,isActive);
            
            if (item == UpgradeType.GRAINATOR) {
                for (int i = 0; i < myParcel.foodList.Length; i++) {
                    plants[i].ShowGrainator(isActive);
                    myParcel.foodList[i].SetGrainatorFood(isActive ? myParcel.foodList[i].grainatorFood : FoodType.NONE);
                }
            }
        }
        
        UIManager.Instance.OpenPanel(parcelMenu);
    }
    
    public void OpenMenuSeed(int foodSlot) {
        foodShop.SetActive(true);
        closeOverlay.SetActive(true);
        
        foodShop.transform.DOMoveX(foodShop.transform.position.x - 300, 0.325f);
        parcelMenu.transform.DOMoveX(parcelMenu.transform.position.x - 150, 0.325f);
        GardenManager.Instance.mySlot = foodSlot;
    }

    public void CloseMenuSeed() {
        closeOverlay.SetActive(false);
        
        foodShop.transform.DOMoveX(foodShop.transform.position.x + 300, 0.325f);
        parcelMenu.transform.DOMoveX(parcelMenu.transform.position.x + 150, 0.325f).OnComplete(() => {
            foodShop.SetActive(false);
        });
    }
}
