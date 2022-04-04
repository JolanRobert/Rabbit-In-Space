using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UIGarden : MonoBehaviour {

    public static UIGarden Instance;

    [Header("Parcel Menu")]
    [SerializeField] private GameObject parcelMenu;
    public List<ParcelMenuEntry> plants;
    
    [Header("Parcel Upgrade")]
    [SerializeField] private GameObject parcelUpgrade;
    public List<ParcelUpgradeEntry> upgrades;
    
    [Header("Food Shop")]
    [SerializeField] private GameObject foodShop;
    [SerializeField] private GameObject closeOverlay;

    [Header("Start Service")]
    [SerializeField] private GameObject servicePanel;
    [SerializeField] private List<ServiceEntry> serviceEntries;

    void Awake() {
        Instance = this;
    }

    public void OpenParcelMenu() {
        //Get Parcel values into ParcelMenu UI Menu
        for (int i = 0; i < plants.Count; i++) {
            GardenManager.Instance.myParcel.foodList[i].foodUI = plants[i];
            GardenManager.Instance.myParcel.foodList[i].InitFoodUI();
        }
        
        GardenManager.Instance.myParcel.upgradesUI = upgrades;
        GardenManager.Instance.myParcel.InitUpgrades();
        
        parcelMenu.SetActive(true);
        parcelMenu.transform.DOScale(1, 0.325f);
    }

    public void CloseMenuParcel() {
        parcelMenu.transform.DOScale(0, 0.325f).OnComplete(() => {
            parcelMenu.gameObject.SetActive(false);
            for (int i = 0; i < plants.Count; i++) GardenManager.Instance.myParcel.foodList[i].foodUI = null;
        });
        
        PlayerManager.Instance.GetInteract().isInteracting = false;
    }

    public void OpenMenuUpgrade() {
        parcelMenu.gameObject.SetActive(false);
        parcelUpgrade.gameObject.SetActive(true);
    }

    public void CloseMenuUpgrade() {
        parcelUpgrade.gameObject.SetActive(false);
        parcelMenu.gameObject.SetActive(true);
    }
    
    public void OpenMenuSeed(int foodSlot) {
        closeOverlay.SetActive(true);
        
        foodShop.transform.DOMoveX(foodShop.transform.position.x - 300, 0.325f);
        parcelMenu.transform.DOMoveX(parcelMenu.transform.position.x - 150, 0.325f);
        GardenManager.Instance.mySlot = foodSlot;
    }

    public void CloseMenuSeed() {
        closeOverlay.SetActive(false);
        
        foodShop.transform.DOMoveX(foodShop.transform.position.x + 300, 0.325f);
        parcelMenu.transform.DOMoveX(parcelMenu.transform.position.x + 150, 0.325f);
    }

    public void OpenService() {
        servicePanel.SetActive(true);
        
        KitchenManager.Instance.myMenu.GenerateMenu();
        List<RecipeSO> myMenu = KitchenManager.Instance.myMenu.todayMenu;
        for (int i = 0; i < myMenu.Count; i++) {
            serviceEntries[i].Init(myMenu[i]);
        }
        
    }

    public void CloseService() {
        servicePanel.SetActive(false);
    }
}
