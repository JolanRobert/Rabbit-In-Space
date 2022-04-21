using System.Collections.Generic;
using UnityEngine;

public class RecipeAmountPrompt : MonoBehaviour {
    
    public static RecipeAmountPrompt Instance;
    [SerializeField] private Counter counter;
    [SerializeField] private GameObject ingredientSlotPrefab;
    [SerializeField] private Transform ingredientGroup;
    private Dictionary<FoodType, IngredientSlot> slots = new Dictionary<FoodType, IngredientSlot>();

    void Awake() {
        Instance = this;
    }

    public void CreateIngredientSlots() {
        ClearPanel();
        counter.Reset();
        
        foreach (RecipeElement element in RecipeManager.Instance.pendingRecipe.recipeElements) {
            IngredientSlot ingSlot = Instantiate(ingredientSlotPrefab, ingredientGroup).GetComponent<IngredientSlot>();
            ingSlot.Init(element,counter.currentValue);
            slots.Add(element.food.foodType, ingSlot);
        }
    }

    public void UpdateIngredientSlots() {
        if (slots.Count == 0) return;
        foreach (RecipeElement element in RecipeManager.Instance.pendingRecipe.recipeElements) {
            slots[element.food.foodType].amountText.text = (element.amount * counter.currentValue).ToString();
        }
    }
    
    public void ConfirmAmountRecipe() {
        RecipeManager.Instance.StartRecipe(counter.currentValue);
    }

    private void ClearPanel() {
        foreach (Transform child in ingredientGroup) Destroy(child.gameObject);
        slots.Clear();
    }
}