using System.Collections.Generic;
using UnityEngine;

public class MenuGenerator : MonoBehaviour {

    public List<RecipeSO> todayMenu;
    [SerializeField] private int nbRecipes;
    [SerializeField] private int minAmountOfRecipeProd;

    public void GenerateMenu() {
        List<RecipeSO> availableRecipes = GetAvailableRecipes();
        if (availableRecipes.Count == 0) {
            todayMenu.Clear();
            return;
        }

        if (availableRecipes.Count > nbRecipes) LimitMenu(availableRecipes);
        todayMenu = availableRecipes;
    }

    private List<RecipeSO> GetAvailableRecipes() {
        List<RecipeSO> availableRecipes = new List<RecipeSO>();
        foreach (RecipeSO rSo in KitchenManager.Instance.recipeList) {
            bool recipe = true;
            foreach (RecipeElement element in rSo.recipeElements) {
                if (!FoodDataManager.Instance.CheckItemQuantity(element.food.foodType, element.amount * minAmountOfRecipeProd)) {
                    recipe = false;
                    break;
                }
            }
            
            if (recipe) availableRecipes.Add(rSo);
        }

        return availableRecipes;
    }

    private void LimitMenu(List<RecipeSO> availableRecipes) {
        while (availableRecipes.Count > nbRecipes) {
            availableRecipes.Remove(availableRecipes[Random.Range(0, availableRecipes.Count)]);
        }
    }

    //Renvoie un plat aléatoire dans le menu du jour
    public RecipeSO GetRandomRecipe() {
        return todayMenu[Random.Range(0,todayMenu.Count)];
    }

    //Renvoie le plat le moins cher du menu
    public RecipeSO GetCheapRecipe() {
        List<RecipeSO> cheapest = new List<RecipeSO>();
        cheapest.Add(todayMenu[0]);

        for (int i = 1; i < todayMenu.Count; i++) {
            if (cheapest[0].goldReward < todayMenu[i].goldReward) continue;
            cheapest.Clear();
            cheapest.Add(todayMenu[i]);
        }
        
        return cheapest[Random.Range(0,cheapest.Count)];
    }
    
    //Renvoie le plat le plus cher du menu
    public RecipeSO GetExpensiveRecipe() {
        List<RecipeSO> cheapest = new List<RecipeSO>();
        cheapest.Add(todayMenu[0]);

        for (int i = 1; i < todayMenu.Count; i++) {
            if (cheapest[0].goldReward > todayMenu[i].goldReward) continue;
            cheapest.Clear();
            cheapest.Add(todayMenu[i]);
        }
        
        return cheapest[Random.Range(0,cheapest.Count)];
    }
    
    //Renvoie un plat aléatoire parmi tous les plats existants
    public RecipeSO GetTrueRandomRecipe() {
        return KitchenManager.Instance.recipeList[Random.Range(0, KitchenManager.Instance.recipeList.Count)];
    }
}
