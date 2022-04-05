using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeAmountPrompt : MonoBehaviour
{
    public static RecipeAmountPrompt instance;
    [SerializeField] private Counter counter;
    [SerializeField] private GameObject ingredientSlotPrefab;
    [SerializeField] private Transform ingredientGroup;
    private Dictionary<FoodType, IngredientSlot> slots = new Dictionary<FoodType, IngredientSlot>();

    void Awake()
    {
        instance = this;
    }
    void OnEnable()
    {
        //CreateIngredientSlots();
    }

    public void CreateIngredientSlots()
    {
        foreach (RecipeElement element in RecipeManager.instance.pendingRecipe.recipeElements)
        {
            slots.Add(element.food.foodType,
                Instantiate(ingredientSlotPrefab, Vector3.zero, Quaternion.identity, ingredientGroup)
                    .GetComponent<IngredientSlot>());
            slots[element.food.foodType].ingredientSprite.sprite = element.food.foodSprite;
            slots[element.food.foodType].amountText.text = (element.amount * counter.value).ToString();
        }
    }

    public void UpdateIngredientSlots()
    {
        foreach (RecipeElement element in RecipeManager.instance.pendingRecipe.recipeElements)
        {
            slots[element.food.foodType].amountText.text = (element.amount * counter.value).ToString();
        }
    }

    public void ClearPanel()
    {
        foreach (Transform child in ingredientGroup.transform) Destroy(child.gameObject);
        slots.Clear();
    }
    
}