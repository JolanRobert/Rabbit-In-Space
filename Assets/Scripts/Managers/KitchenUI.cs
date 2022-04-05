using UnityEngine;

public class KitchenUI : MonoBehaviour {

    public static KitchenUI Instance;
    private GameObject currentPanel;

    [Header("Recipe Prompts")] [SerializeField]
    private GameObject overrideRecipePrompt, amountRecipePrompt;
    void Awake() {
        Instance = this;
    }

    public void OpenBook(GameObject go)
    {
        PlayerManager.Instance.GetInteract().isInteracting = true;
        currentPanel = go;
        currentPanel.SetActive(true);
    }

    public void OpenOverrideRecipePrompt()
    {
        PlayerManager.Instance.GetInteract().isInteracting = true;
        currentPanel = overrideRecipePrompt;
        currentPanel.SetActive(true);
    }
    public void ConfirmOverrideRecipe()
    {
        RecipeManager.instance.EndRecipe(false);
        RecipeManager.instance.PromptRecipeAmount();
        ClosePanel();
    }

    public void OpenAmountRecipePrompt(RecipeSO recipe)
    {
        PlayerManager.Instance.GetInteract().isInteracting = true;
        currentPanel = amountRecipePrompt;
        currentPanel.SetActive(true);
        RecipeAmountPrompt.instance.CreateIngredientSlots();
    }
    public void ConfirmAmountRecipe(Counter counter)
    {
        RecipeManager.instance.StartRecipe(counter.value);
    }
    public void OpenFridge()
    {
        currentPanel = InventoryManager.fridgeInstance.inventory;
        InventoryManager.fridgeInstance.OpenInventory();
    }
    
    public void OpenWorkplan()
    {
        currentPanel = InventoryManager.workplanInstance.inventory;
        InventoryManager.workplanInstance.OpenInventory();
    }
    
    public void ClosePanel() {
        currentPanel.SetActive(false);
        currentPanel = null;
        PlayerManager.Instance.GetInteract().isInteracting = false;
    }
}