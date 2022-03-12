using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class RecipePanel : MonoBehaviour
{
    [Header("Components")] 
    [SerializeField] private Transform ingredientGroup;
    [SerializeField] private Transform stationsGroup;
    [SerializeField] private GameObject ingredientSlotPrefab;
    [SerializeField] private GameObject stationSlotPrefab;
    private RecipeSO recipe;
    [SerializeField] private string name;
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text nameText, priceText;
    [Header("Attributes")]
    public ItemType type;
    public int price;

    public void SetupPanel(RecipeSO newRecipe)
    {
        recipe = newRecipe;
        type = recipe.itemType;
        image.sprite = recipe.foodSprite;
        name = recipe.name;
        nameText.text = name;
        price = recipe.price;
        priceText.text = recipe.price + "$";
        foreach (RecipeElement element in recipe.recipeElements)
        {
            IngredientSlot slot = Instantiate(ingredientSlotPrefab, Vector3.zero, Quaternion.identity, ingredientGroup)
                .GetComponent<IngredientSlot>();
            slot.ingredientSprite.sprite = element.food.foodSprite;
            slot.amountText.text = element.amount.ToString();
        }
        foreach (StationSO station in recipe.stations)
        {
            StationSlot slot = Instantiate(stationSlotPrefab, Vector3.zero, Quaternion.identity, stationsGroup).GetComponent<StationSlot>();
            slot.image.sprite = station.icon;
        }
    }

    public void StartRecipe()
    {
        foreach (RecipeElement element in recipe.recipeElements)
        {
            if(!FoodDataManager.Instance.CheckItemQuantity(element.food.itemType,element.amount))
            {
                Debug.Log("Not enough " + element.food.name);
                return;
            }
        }
        foreach (RecipeElement element in recipe.recipeElements)
        {
            FoodDataManager.Instance.AddItems(element.food.itemType, -element.amount);
            //Debug.Log("Took " + element.amount + " " + element.food.name);
        }
        
        RecipeManager.instance.StartRecipeTimeline(recipe);
    }
}