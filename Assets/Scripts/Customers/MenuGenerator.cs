using System.Collections.Generic;
using UnityEngine;

public class MenuGenerator : MonoBehaviour {

    public List<RecipeSO> todayMenu;
    [SerializeField] private int nbRecipes;
    [SerializeField] private int minAmountOfRecipeProd;

    public void GenerateMenu() {
        List<RecipeSO> availableRecipes = GetAvailableRecipes();
        if (availableRecipes.Count == 0) {
            Debug.Log("Aucun plat ne correspond aux critères de lancement d'un service !");
            return;
        }

        if (availableRecipes.Count > nbRecipes) LimitMenu(availableRecipes);
        todayMenu = availableRecipes;
    }

    private List<RecipeSO> GetAvailableRecipes() {
        List<RecipeSO> availableRecipes = new List<RecipeSO>();
        foreach (RecipeSO rSo in KitchenManager.Instance.recipeList) {
            bool recipe = true;
            foreach (RecipeElement re in rSo.recipeElements) {
                if (!FoodDataManager.Instance.CheckItemQuantity(re.food.foodType, re.amount * minAmountOfRecipeProd)) {
                    recipe = false;
                    break;
                }
            }
            
            if (recipe) availableRecipes.Add(rSo);
            else Debug.Log("Can't add "+rSo.name+" to today's Menu");
        }

        return availableRecipes;
    }

    private void LimitMenu(List<RecipeSO> availableRecipes) {
        while (availableRecipes.Count > nbRecipes) {
            availableRecipes.Remove(availableRecipes[Random.Range(0, availableRecipes.Count)]);
        }
    }

    public List<RecipeSO> GetMenu() {
        return todayMenu;
    }
}
