using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class InventoryManager : MonoBehaviour {
    
    public static InventoryManager fridgeInstance;
    public static InventoryManager workplanInstance;
    
    [SerializeField] private StockType stockType;
    public GameObject inventory;
    [SerializeField] private GameObject slotPrefab;
    
    private Dictionary<FoodType, InventorySlot> fridgeInventory = new Dictionary<FoodType, InventorySlot>();
    private Dictionary<RecipeType, InventorySlot> workplanInventory = new Dictionary<RecipeType, InventorySlot>();
    
    
    
    private void Awake() {
        switch (stockType) {
            case StockType.FRIDGE:
                if (fridgeInstance != null) return;
                fridgeInstance = this;
                break;
            case StockType.WORKPLAN:
                if (workplanInstance != null) return;
                workplanInstance = this;
                break;
        }
    }
    
    private void Start() {
        switch (stockType) {
            case StockType.FRIDGE:
                foreach (FoodSO fSo in KitchenManager.Instance.foodList) {
                    GameObject slot = Instantiate(slotPrefab, transform.GetChild(0).GetChild(0));
                    AddFridgeSlot(fSo.foodType, slot.GetComponent<InventorySlot>(), fSo.foodSprite);
                }
                break;
            
            case StockType.WORKPLAN:
                foreach (RecipeSO rSo in KitchenManager.Instance.recipeList) {
                    GameObject slot = Instantiate(slotPrefab, transform.GetChild(0).GetChild(0));
                    AddWorkplanSlot(rSo.recipeType, slot.GetComponent<InventorySlot>(), rSo.recipeSprite);
                }
                break;
        }
    }
    
    private void AddFridgeSlot(FoodType type, InventorySlot slot, Sprite sprite) {
        if (fridgeInventory.ContainsKey(type)) return;
        fridgeInventory.Add(type, slot);
        slot.SetupSlot(type, sprite);
    }
    
    private void AddWorkplanSlot(RecipeType type, InventorySlot slot, Sprite sprite) {
        if (workplanInventory.ContainsKey(type)) return;
        workplanInventory.Add(type, slot);
        slot.SetupSlot(type, sprite);
    }
    
    public void OpenInventory() {
        inventory.SetActive(true);
    }
    
    [Serializable]
    public class FoodItem {
        public FoodType foodType;
        [XmlIgnore]
        public Sprite sprite;
        public int amount;
    }
    
    [Serializable]
    public class RecipeItem {
        public RecipeSO rSo;
        public int amount;

        public RecipeItem(RecipeSO rSo) {
            this.rSo = rSo;
            amount = 0;
        }
    }
}


