using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipePanel : MonoBehaviour
{
    [Header("Components")] 
    [SerializeField] private string name;
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text nameText, priceText;
    [Header("Attributes")]
    public ItemType type;
    public int price;

    public void SetupPanel(RecipeSO recipe)
    {
        type = recipe.itemType;
        image.sprite = recipe.foodSprite;
        name = recipe.name;
        nameText.text = name;
        price = recipe.price;
        priceText.text = recipe.price + "$";
    }

    public void StartRecipe()
    {
        Debug.Log("Start " + name);
    }
}