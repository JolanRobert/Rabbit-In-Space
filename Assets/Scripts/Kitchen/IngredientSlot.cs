using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngredientSlot : MonoBehaviour {
    
    public Image slotSprite;
    public TMP_Text slotAmount;

    public void Init(RecipeElement element, int counterValue) {
        slotSprite.sprite = element.food.foodSprite;
        slotAmount.text = "x" + element.amount * counterValue;
    }
}
