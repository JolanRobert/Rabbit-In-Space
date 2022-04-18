using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {
    
    [Header("Components")]
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text amountText;

    public void Init(FoodSO fSo) {
        image.sprite = fSo.foodSprite;
        amountText.text = "0";
        UpdateAmount(InventoryManager.Instance.GetAmountOfFoodItem(fSo));
    }

    public void Init(RecipeSO rSo) {
        image.sprite = rSo.recipeSprite;
        amountText.text = "0";
        UpdateAmount(InventoryManager.Instance.GetAmountOfRecipeItem(rSo));
    }

    public void UpdateAmount(int amount) {
        amountText.text = amount.ToString();
    }
}
