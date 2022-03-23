using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeTimeline : MonoBehaviour
{
    [SerializeField] private Transform stationsGroup;
    [SerializeField] private GameObject stationSlotPrefab;
    [SerializeField] private Image recipeImage;

    public void ShowRecipeTimeline(RecipeSO recipe)
    {
        recipeImage.sprite = recipe.recipeSprite;
        foreach (StationSO station in recipe.stations)
        {
            GameObject stationSlot = Instantiate(stationSlotPrefab, Vector3.zero, Quaternion.identity, stationsGroup);
            stationSlot.GetComponent<StationSlot>().image.sprite = station.icon;
        }
    }
}
