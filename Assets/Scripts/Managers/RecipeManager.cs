using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public static RecipeManager instance;
    
    [SerializeField] private GameObject recipeTimeline;
    [SerializeField] private RecipeSO currentRecipe;
    private Queue<StationType> stations = new Queue<StationType>();
    
    public List<InventoryManager.RecipeItem> serviceRecipes = new List<InventoryManager.RecipeItem>();

    void Awake() {
        instance = this;
    }

    void Start() {
        foreach (RecipeSO rSo in KitchenManager.Instance.recipeList) {
            serviceRecipes.Add(new InventoryManager.RecipeItem(rSo));
        }
    }

    public void InitRecipeTimeline(RecipeSO recipe) {
        
        if (currentRecipe != null) EndRecipe(false);
        
        currentRecipe = recipe;
        stations.Clear();
        
        foreach (StationSO station in recipe.stations) stations.Enqueue(station.stationType);
        
        Debug.Log(currentRecipe.name + " recipe has started.");
        
        KitchenUI.Instance.ClosePanel();
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
            foreach (InventoryManager.RecipeItem recipe in serviceRecipes) {
                if (currentRecipe != recipe.rSo) continue;
                recipe.amount += 1;
                break;
            }
            
            Debug.Log(currentRecipe.name + " recipe has ended with success.");
        }
        else Debug.Log(currentRecipe.name + " recipe has ended with failure.");
        
        currentRecipe = null;
        foreach (Transform child in recipeTimeline.transform.GetChild(1)) Destroy(child.gameObject);
        recipeTimeline.gameObject.SetActive(false);
    }
    
    public bool CheckIsNextStation(StationType type) {
        if (stations.Count == 0) {
            Debug.Log("No recipe in progress...");
            return false;
        }
        
        if (stations.Peek() == type) return true;
        Debug.Log(type + " is not the anticipated station.");
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
}
