using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RecipeManager : MonoBehaviour {
    
    public static RecipeManager Instance;

    [SerializeField] private RecipeSummary recipeSummary;
    
    public RecipeSO currentRecipe, pendingRecipe;
    public RecipePanel pendingRecipePanel;
    
    [SerializeField] private GameObject recipeTimeline;
    public int recipeAmount;
    public int recipePanelIndex;
    
    private Queue<StationType> stations = new Queue<StationType>();

    void Awake() {
        if (Instance != null) Destroy(gameObject);
        else Instance = this;
    }
    
    //
    // RECIPE
    //
    
    public void TryStartRecipe(RecipeSO recipe, RecipePanel recipePanel) {
        pendingRecipe = recipe;
        pendingRecipePanel = recipePanel;
        
        //On propose d'override la recette déjà existante
        if (currentRecipe != null) {
            UIManager.Instance.OpenPanel(UIKitchen.Instance.recipeOverridePrompt);
            return;
        }
        
        PromptAmountRecipe();
    }
    
    
    public void StartRecipe(int newRecipeAmount) {
        recipeAmount = newRecipeAmount;
        foreach (RecipeElement element in pendingRecipe.recipeElements) {
            if(!FoodDataManager.Instance.CheckItemQuantity(element.food.foodType,element.amount * recipeAmount)) {
                Debug.Log("Not enough " + element.food.name);
                return;
            }
        }
        foreach (RecipeElement element in pendingRecipe.recipeElements) {
            FoodDataManager.Instance.AddItem(element.food.foodType, -element.amount * recipeAmount);
            //Debug.Log("Took " + element.amount + " " + element.food.name);
        }

        recipePanelIndex = pendingRecipePanel.transform.GetSiblingIndex();
        pendingRecipePanel.SetAsRunning(recipeAmount);
        InitStationsTimeline(pendingRecipe);
    }

    public void EndRecipe(bool success) {
        if (success) {
            for (int i = 0; i < InventoryManager.Instance.recipeItems.Count; i++) {
                FoodDataManager.RecipeItem item = InventoryManager.Instance.recipeItems[i];
                if (item.recipeType != currentRecipe.recipeType) continue;
                item.amount += 1 * recipeAmount;
                break;
            }
            StartCoroutine(WaitForSuccessRecipe());

            //Debug.Log(currentRecipe.name + " recipe has ended with success.");
        }
        //else Debug.Log(currentRecipe.name + " recipe has ended with failure.");
        
        currentRecipe = null;
        stations = new Queue<StationType>();
        StartCoroutine(ResetBook());
        StartCoroutine(WaitForGlow());
    }
    public IEnumerator WaitForSuccessRecipe()
    {
        while (SceneManager.GetActiveScene().name != "Kitchen")
        {
            yield return null;
        }
        recipeSummary.OpenSummary(currentRecipe);
    }

    IEnumerator ResetBook()
    {
        while (SceneManager.GetActiveScene().name != "Kitchen")
        {
            yield return null;
        }
        PrefabManager.Instance.ResetRecipeBook();
    }

    private void InitStationsTimeline(RecipeSO recipe) {
        if (currentRecipe != null) EndRecipe(false);
        currentRecipe = recipe;
        stations.Clear();
        
        foreach (StationSO station in recipe.stations) stations.Enqueue(station.stationType);
        SeeNextStep();
    }

    public bool CheckIsNextStation(StationType type) {
        if (stations.Count == 0) {
            //Debug.Log("No recipe in progress...");
            return false;
        }
        
        if (stations.Peek() == type) return true;
        //Debug.Log(type + " is not the anticipated station.");
        return false;
    }
    
    public void ForwardStep() {
        stations.Dequeue();
        if (stations.Count == 0) {
            EndRecipe(true);
            return;
        }
        StartCoroutine(WaitForGlow());
    }

    public IEnumerator WaitForGlow()
    {
        while (SceneManager.GetActiveScene().name != "Kitchen")
        {
            yield return null;
        }
        SeeNextStep();
    }

    private void SeeNextStep()
    {
        if (stations.Count == 0)
        {
            Station.OnStepChange.Invoke(default);
        }
        else
        {
            Station.OnStepChange.Invoke(stations.Peek());
        }
        
    }

    public void PromptAmountRecipe() {
        UIManager.Instance.OpenPanel(UIKitchen.Instance.recipeAmountPrompt);
        RecipeAmountPrompt.Instance.CreateIngredientSlots();
    }
}
