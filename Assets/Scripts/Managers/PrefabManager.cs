using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PrefabManager : MonoBehaviour {
    
    [Header("FoodShop")]
    [SerializeField] private GameObject foodShopEntryPrefab;
    [SerializeField] private Transform foodShopEntryParent;

    [Header("ParcelUpgrade")]
    [SerializeField] private GameObject parcelUpgradeEntryPrefab;
    [SerializeField] private Transform parcelUpgradeEntryParent;
    
    [Header("Fridge/Workplan")]
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform slotFridgeParent;
    [SerializeField] private Transform slotWorkplanParent;
    
    [Header("RecipeBook")]
    [SerializeField] private GameObject recipePanelPrefab;
    [SerializeField] private Transform recipePanelGroup;

    [Header("Directory")]
    [SerializeField] private GameObject customerEntryPrefab;
    [SerializeField] private Transform customerEntryGroup;

    void Start() {
        foreach (FoodSO fSo in DataManager.Instance.foodList) {
            FoodShopEntry entry = Instantiate(foodShopEntryPrefab, foodShopEntryParent).GetComponent<FoodShopEntry>();
            entry.Init(fSo);
            
            InventorySlot slot = Instantiate(slotPrefab, slotFridgeParent).GetComponent<InventorySlot>();
            slot.Init(fSo);
            UIKitchen.Instance.fridgeSlots.Add(slot);
        }
        
        foreach (ParcelUpgradeSO puSo in DataManager.Instance.parcelUpgradeList) {
            ParcelUpgradeEntry entry = Instantiate(parcelUpgradeEntryPrefab, parcelUpgradeEntryParent).GetComponent<ParcelUpgradeEntry>();
            entry.Init(puSo);
            UIGarden.Instance.upgrades.Add(entry);
        }

        foreach (RecipeSO rSo in KitchenManager.Instance.recipeList) {
            RecipePanel recipePanel = Instantiate(recipePanelPrefab, recipePanelGroup).GetComponent<RecipePanel>();
            recipePanel.SetupPanel(rSo);
            
            InventorySlot slot = Instantiate(slotPrefab, slotWorkplanParent).GetComponent<InventorySlot>();
            slot.Init(rSo);
            UIKitchen.Instance.workplanSlots.Add(slot);
        }

        foreach (CustomerSO cSo in DataManager.Instance.customerList) {
            CustomerEntry customerEntry = Instantiate(customerEntryPrefab, customerEntryGroup).GetComponent<CustomerEntry>();
            customerEntry.Init(cSo);
        }
    }
}
