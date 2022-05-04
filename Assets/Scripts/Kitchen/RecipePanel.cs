using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipePanel : MonoBehaviour
{
    [Header("Prefabs")] 
    [SerializeField] private GameObject ingredientSlotPrefab;
    [SerializeField] private Transform ingredientsGroup;
    [SerializeField] private GameObject stationSlotPrefab;
    [SerializeField] private Transform stationsGroup;
    [SerializeField] private Transform cancelGroup;

    [Header("Panel Infos")]
    private RecipeSO myRecipe;
    [SerializeField] private Image recipeImage, panelImage;
    [SerializeField] private TMP_Text nameText, priceText, makingAmountText;

    [Header("Attributes")]
    private bool isRunning;
    private int originalOrderInHierarchy;
    [SerializeField] private Color runningColor;
    [SerializeField] private Color inactiveColor;

    public void SetupPanel(RecipeSO newRecipe) {
        myRecipe = newRecipe;
        
        recipeImage.sprite = myRecipe.recipeSprite;
        nameText.text = newRecipe.name;
        priceText.text = myRecipe.goldReward + "$";
        
        foreach (RecipeElement element in myRecipe.recipeElements) {
            IngredientSlot slot = Instantiate(ingredientSlotPrefab, Vector3.zero, Quaternion.identity, ingredientsGroup).GetComponent<IngredientSlot>();
            slot.Init(element,1);
        }
        
        foreach (StationSO station in myRecipe.stations) {
            StationSlot slot = Instantiate(stationSlotPrefab, Vector3.zero, Quaternion.identity, stationsGroup).GetComponent<StationSlot>();
            slot.Init(station);
        }

        originalOrderInHierarchy = transform.GetSiblingIndex();
    }

    public void SetAsRunning(int amount) {
        isRunning = true;
        panelImage.color = runningColor;
        transform.SetSiblingIndex(0);
        ingredientsGroup.gameObject.SetActive(false);
        cancelGroup.gameObject.SetActive(true);
        makingAmountText.text = $"Making (x{amount})";
    }

    public void SetAsNotRunning() {
        isRunning = false;
        panelImage.color = inactiveColor;
        transform.SetSiblingIndex(originalOrderInHierarchy);
        ingredientsGroup.gameObject.SetActive(true);
        cancelGroup.gameObject.SetActive(false);
    }

    public void StartRecipe() {
        if (!KitchenManager.Instance.inService) return;
        if (isRunning) return;
        RecipeManager.Instance.TryStartRecipe(myRecipe, this);
    }

    public void CancelRecipe()
    {
        RecipeManager.Instance.EndRecipe(false);
        SetAsNotRunning();
    }
}