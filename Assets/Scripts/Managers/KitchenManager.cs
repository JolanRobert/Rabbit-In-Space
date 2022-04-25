using System.Collections.Generic;
using UnityEngine;

public class KitchenManager : MonoBehaviour {

    public static KitchenManager Instance;
    public MenuGenerator myMenu;
    public CustomerSpawner customerSpawner;

    public List<FoodSO> foodList;
    public List<RecipeSO> recipeList;

    public bool inService;

    void Awake() {
        if (Instance != null) Destroy(gameObject);
        else Instance = this;
    }
}
