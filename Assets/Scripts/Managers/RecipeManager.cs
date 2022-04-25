using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour {
    
    public static RecipeManager Instance;
    
    public RecipeSO currentRecipe, pendingRecipe;
    
    [SerializeField] private GameObject recipeTimeline;
    private int recipeAmount;
    
    private Queue<StationType> stations = new Queue<StationType>();

    void Awake() {
        if (Instance != null) Destroy(gameObject);
        else Instance = this;
    }
    
    //
    // RECIPE
    //
    
    public void TryStartRecipe(RecipeSO recipe) {
        pendingRecipe = recipe;
        
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
        InitRecipeTimeline(pendingRecipe);
    }
    
    public void EndRecipe(bool success) {
        if (success) {
            for (int i = 0; i < InventoryManager.Instance.recipeItems.Count; i++) {
                FoodDataManager.RecipeItem item = InventoryManager.Instance.recipeItems[i];
                if (item.recipeType != currentRecipe.recipeType) continue;
                item.amount += 1 * recipeAmount;
                break;
            }
            
            //Debug.Log(currentRecipe.name + " recipe has ended with success.");
        }
        //else Debug.Log(currentRecipe.name + " recipe has ended with failure.");
        
        currentRecipe = null;
        foreach (Transform child in recipeTimeline.transform.GetChild(1)) Destroy(child.gameObject);
        recipeTimeline.gameObject.SetActive(false);
    }
    
    //
    // RECIPE TIMELINE
    //

    private void InitRecipeTimeline(RecipeSO recipe) {
        if (currentRecipe != null) EndRecipe(false);
        currentRecipe = recipe;
        stations.Clear();
        
        foreach (StationSO station in recipe.stations) stations.Enqueue(station.stationType);
        
        //Debug.Log(currentRecipe.name + " recipe has started.");
        
        ShowRecipeTimeline();
        recipeTimeline.GetComponent<RecipeTimeline>().ShowRecipeTimeline(recipe);
        SeeNextStep();
    }

    public void ShowRecipeTimeline() {
        recipeTimeline.gameObject.SetActive(true);
    }

    public void HideRecipeTimeline() {
        recipeTimeline.gameObject.SetActive(false);
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
        SeeNextStep();
    }

    private void SeeNextStep() {
        Debug.Log("Next step is : " + stations.Peek());
    }

    public void PromptAmountRecipe() {
        UIManager.Instance.OpenPanel(UIKitchen.Instance.recipeAmountPrompt);
        RecipeAmountPrompt.Instance.CreateIngredientSlots();
    }
}
