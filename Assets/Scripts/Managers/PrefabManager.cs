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

    void Start() {
        foreach (FoodSO foodSo in DataManager.Instance.foodList) {
            FoodShopEntry entry = Instantiate(foodShopEntryPrefab, foodShopEntryParent).GetComponent<FoodShopEntry>();
            entry.Init(foodSo);
        }
        
        foreach (ParcelUpgradeSO puSo in DataManager.Instance.parcelUpgradeList) {
            ParcelUpgradeEntry entry = Instantiate(parcelUpgradeEntryPrefab, parcelUpgradeEntryParent).GetComponent<ParcelUpgradeEntry>();
            entry.Init(puSo);
            UIGarden.Instance.upgrades.Add(entry);
        }

        foreach (FoodSO fSo in KitchenManager.Instance.foodList) {
            InventorySlot slot = Instantiate(slotPrefab, slotFridgeParent).GetComponent<InventorySlot>();
            slot.Init(fSo);
            UIKitchen.Instance.fridgeSlots.Add(slot);
        }

        foreach (RecipeSO rSo in KitchenManager.Instance.recipeList) {
            InventorySlot slot = Instantiate(slotPrefab, slotWorkplanParent).GetComponent<InventorySlot>();
            slot.Init(rSo);
            UIKitchen.Instance.workplanSlots.Add(slot);
        }
    }
}
