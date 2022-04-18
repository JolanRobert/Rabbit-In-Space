using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {
    
    public static InventoryManager Instance;

    public List<FoodDataManager.FoodItem> foodItems = new List<FoodDataManager.FoodItem>();
    public List<FoodDataManager.RecipeItem> recipeItems = new List<FoodDataManager.RecipeItem>();

    void Awake() {
        if (Instance != null) Destroy(gameObject);
        else {
            Instance = this;
        }
    } 

    void Start() {
        InitFridge();
        InitWorkplan();
    }

    private void InitFridge() {
        foreach (FoodSO fSo in DataManager.Instance.foodList) {
            FoodDataManager.FoodItem foodItem = FoodDataManager.Instance.Load(fSo.foodType.ToString());
            if (foodItem == null) foodItem = new FoodDataManager.FoodItem(fSo.foodType);
            foodItems.Add(foodItem);
        }
    }

    private void InitWorkplan() {
        foreach (RecipeSO rSo in DataManager.Instance.recipeList) {
            recipeItems.Add(new FoodDataManager.RecipeItem(rSo.recipeType));
        }
    }
    
    public int GetAmountOfFoodItem(FoodSO fSo) {
        foreach (FoodDataManager.FoodItem foodItem in foodItems) {
            if (foodItem.foodType != fSo.foodType) continue;
            return foodItem.amount;
        }
        return 0;
    }
    
    public int GetAmountOfRecipeItem(RecipeSO rSo) {
        foreach (FoodDataManager.RecipeItem recipeItem in recipeItems) {
            if (recipeItem.recipeType != rSo.recipeType) continue;
            return recipeItem.amount;
        }
        return 0;
    }
}
