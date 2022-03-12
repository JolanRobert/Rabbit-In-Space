using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public static RecipeManager instance;
    [SerializeField] private Transform canvas;
    [SerializeField] private RecipeTimeline recipeTimeline;
    [SerializeField] private RecipeSO currentRecipe;
    private Queue<StationType> stations = new Queue<StationType>();

    void Awake()
    {
        instance = this;
    }

    public void StartRecipeTimeline(RecipeSO recipe)
    {
        if (currentRecipe != null)
        {
            EndRecipe(false);
        }
        currentRecipe = recipe;
        stations.Clear();
        foreach (StationSO station in recipe.stations)
        {
            stations.Enqueue(station.stationType);
        }
        Debug.Log(currentRecipe.name + " recipe has started.");
        UIManager.Instance.ClosePanel();
        recipeTimeline.gameObject.SetActive(true);
        recipeTimeline.ShowRecipeTimeline(recipe);
        SeeNextStep();
    }

    public void EndRecipe(bool success)
    {
        if (success)
        {
            FoodDataManager.Instance.AddItems(currentRecipe.itemType, 1);
            Debug.Log(currentRecipe.name + " recipe has ended with success.");
        }
        else
        {
            Debug.Log(currentRecipe.name + " recipe has ended with failure.");
        }
        currentRecipe = null;
        foreach (Transform child in recipeTimeline.transform.GetChild(1))
        {
            Destroy(child.gameObject);
        }
        recipeTimeline.gameObject.SetActive(false);
    }
    public bool CheckIsNextStation(StationType type)
    {
        if (stations.Count == 0)
        {
            Debug.Log("No recipe in progress...");
            return false;
        }
        if (stations.Peek() == type)
        {
            return true;
        }
        return false;
    }
    public void ForwardStep()
    {
        stations.Dequeue();
        if (stations.Count == 0)
        {
            EndRecipe(true);
            return;
        }
        SeeNextStep();
    }

    private void SeeNextStep()
    {
        Debug.Log("Next step is : " + stations.Peek());
    }
}
