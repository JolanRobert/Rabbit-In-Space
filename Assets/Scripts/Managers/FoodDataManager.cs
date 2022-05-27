using System;
using UnityEngine;

public class FoodDataManager : MonoBehaviour {
    
    public static FoodDataManager Instance;
    
    [SerializeField] private DataSerializer dataSerializer;
    
    void Awake() {
        Instance = this;
    }

    public void SetItem(FoodType foodType, int amount) {
        for (int i = 0; i < InventoryManager.Instance.foodItems.Count; i++) {
            FoodItem item = InventoryManager.Instance.foodItems[i];
            
            if (item.foodType != foodType) continue;
            item.amount = amount;
            UIKitchen.Instance.UpdateFridgeSlot(i,item.amount);
            Save(item.foodType.ToString(),item);
            break;
        }
    }
    
    public void AddItem(FoodType foodType, int amount) {
        for (int i = 0; i < InventoryManager.Instance.foodItems.Count; i++) {
            FoodItem item = InventoryManager.Instance.foodItems[i];
            
            if (item.foodType != foodType) continue;
            item.amount += amount;
            UIKitchen.Instance.UpdateFridgeSlot(i,item.amount);
            Save(item.foodType.ToString(),item);
            break;
        }
    }

    public void AddRecipe(RecipeType recipeType, int amount) {
        for (int i = 0; i < InventoryManager.Instance.recipeItems.Count; i++) {
            RecipeItem item = InventoryManager.Instance.recipeItems[i];
            
            if (item.recipeType != recipeType) continue;
            item.amount += amount;
            UIKitchen.Instance.UpdateWorkplanSlot(i,item.amount);
            CustomerOrderManager.Instance.UpdateCustomersOwnedRecipeAmount();
            break;
        }
    }

    public bool CheckItemQuantity(FoodType foodType, int amount) {
        if (foodType == FoodType.NONE) {
            Debug.LogWarning("Invalid type");
            return false;
        }
        
        foreach (FoodItem item in InventoryManager.Instance.foodItems) {
            if (item.foodType != foodType) continue;
            return amount <= item.amount;
        }

        return false;
    }

    public bool HasRecipeItem(RecipeType recipeType) {
        foreach (RecipeItem item in InventoryManager.Instance.recipeItems) {
            if (item.recipeType != recipeType) continue;
            return item.amount > 0;
        }

        return false;
    }
    
    private void Save(string fileName, FoodItem foodItem) {
        dataSerializer.SaveData(fileName,foodItem);
    }

    public FoodItem Load(string fileName) {
        return dataSerializer.LoadData<FoodItem>(fileName);
    }
    
    
    [Serializable]
    public class FoodItem {
        public FoodType foodType;
        public int amount;
        
        public FoodItem(){}
        
        public FoodItem(FoodType foodType) {
            this.foodType = foodType;
        }
    }
    
    [Serializable]
    public class RecipeItem {
        public RecipeType recipeType;
        public int amount;

        public RecipeItem(RecipeType recipeType) {
            this.recipeType = recipeType;
        }
    }
}
