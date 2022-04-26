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
    private RecipeSO recipe;
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text nameText, priceText;

    [Header("Attributes")] 
    private bool active = true;
    public RecipeType type;
    public int price;
    private int originalOrderInHierarchy;

    public void SetupPanel(RecipeSO newRecipe) {
        recipe = newRecipe;
        type = recipe.recipeType;
        image.sprite = recipe.recipeSprite;
        image.preserveAspect = true;
        name = recipe.name;
        nameText.text = name;
        price = recipe.price;
        priceText.text = recipe.price + "$";
        
        foreach (RecipeElement element in recipe.recipeElements) {
            IngredientSlot slot = Instantiate(ingredientSlotPrefab, Vector3.zero, Quaternion.identity, ingredientsGroup).GetComponent<IngredientSlot>();
            slot.Init(element,1);
        }
        
        foreach (StationSO station in recipe.stations) {
            StationSlot slot = Instantiate(stationSlotPrefab, Vector3.zero, Quaternion.identity, stationsGroup).GetComponent<StationSlot>();
            slot.Init(station);
        }

        originalOrderInHierarchy = transform.GetSiblingIndex();
    }

    public void SetAsRunning()
    {
        active = false;
        transform.SetSiblingIndex(0);
        ingredientsGroup.gameObject.SetActive(false);
        cancelGroup.gameObject.SetActive(true);
    }

    public void SetAsInactive()
    {
        active = true;
        transform.SetSiblingIndex(originalOrderInHierarchy);
        ingredientsGroup.gameObject.SetActive(true);
        cancelGroup.gameObject.SetActive(false);
    }

    public void StartRecipe()
    {
        if (!active) return;
        RecipeManager.Instance.TryStartRecipe(recipe, this);
    }

    public void CancelRecipe()
    {
        RecipeManager.Instance.EndRecipe(false);
        SetAsInactive();
    }
}