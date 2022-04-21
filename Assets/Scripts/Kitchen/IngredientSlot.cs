using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngredientSlot : MonoBehaviour {
    
    [SerializeField] public Image ingredientSprite;
    [SerializeField] public TMP_Text amountText;

    public void Init(RecipeElement element, int counterValue) {
        ingredientSprite.sprite = element.food.foodSprite;
        amountText.text = (element.amount * counterValue).ToString();
    }
}
