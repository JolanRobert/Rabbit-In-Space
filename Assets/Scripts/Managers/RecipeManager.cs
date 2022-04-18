using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour {
    
    public static RecipeManager Instance;
    
    [SerializeField] private GameObject recipeTimeline;
    [SerializeField] private RecipeSO currentRecipe;
    private Queue<StationType> stations = new Queue<StationType>();

    void Awake() {
        if (Instance != null) Destroy(gameObject);
        else {
            Instance = this;
        }
    }

    public void InitRecipeTimeline(RecipeSO recipe) {
        
        if (currentRecipe != null) EndRecipe(false);
        
        currentRecipe = recipe;
        stations.Clear();
        
        foreach (StationSO station in recipe.stations) stations.Enqueue(station.stationType);
        
        //Debug.Log(currentRecipe.name + " recipe has started.");
        
        UIManager.Instance.ClosePanel();
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

    private void EndRecipe(bool success) {
        if (success) {
            for (int i = 0; i < InventoryManager.Instance.recipeItems.Count; i++) {
                FoodDataManager.RecipeItem item = InventoryManager.Instance.recipeItems[i];
                if (item.recipeType != currentRecipe.recipeType) continue;
                item.amount += 1;
                break;
            }
            
            //Debug.Log(currentRecipe.name + " recipe has ended with success.");
        }
        else //Debug.Log(currentRecipe.name + " recipe has ended with failure.");
        
        currentRecipe = null;
        foreach (Transform child in recipeTimeline.transform.GetChild(1)) Destroy(child.gameObject);
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
        //Debug.Log("Next step is : " + stations.Peek());
    }
}
