using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeTimeline : MonoBehaviour {
    
    [SerializeField] private Transform stationsGroup;
    [SerializeField] private GameObject stationSlotPrefab;
    [SerializeField] private Image recipeImage;
    [SerializeField] private TMP_Text amountText;

    public void ShowRecipeTimeline(RecipeSO recipe, int amount)
    {
        recipeImage.sprite = recipe.recipeSprite;
        amountText.text = amount.ToString();
        foreach (StationSO station in recipe.stations)
        {
            GameObject stationSlot = Instantiate(stationSlotPrefab, stationsGroup);
            stationSlot.GetComponent<BookStationSlot>().slotSprite.sprite = station.icon;
        }
    }
}
