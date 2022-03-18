using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {
    
    [Header("Components")]
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text amountText;
    [Header("Attributes")]
    public FoodType foodType;
    public RecipeType recipeType;

    private void OnEnable() {
        UpdateSlot();
    }

    public void SetupSlot(FoodType newType, Sprite sprite) {
        foodType = newType;
        image.sprite = sprite;
        UpdateSlot();
    }

    public void SetupSlot(RecipeType newType, Sprite sprite) {
        recipeType = newType;
        image.sprite = sprite;
        UpdateSlot();
    }

    private void UpdateSlot() {
        if (foodType != FoodType.NONE) {
            amountText.text = FoodDataManager.Instance.Load(foodType.ToString()).amount.ToString();
        }
        else {
            foreach (InventoryManager.RecipeItem item in InventoryManager.workplanInstance.serviceRecipes) {
                if (item.rSo.recipeType != recipeType) continue;
                amountText.text = ""+item.amount;
                break;
            }
        }
    }
}
