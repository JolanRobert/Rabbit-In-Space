using System.Collections.Generic;
using UnityEngine;

public class KitchenManager : MonoBehaviour {

    public static KitchenManager Instance;
    public MenuGenerator myMenu;
    public CustomerSpawner customerSpawner;

    public List<FoodSO> foodList;
    public List<RecipeSO> recipeList;
    public List<CustomerSO> customerList;
    
    public bool inService;

    void Awake() {
        Instance = this;
        myMenu = GetComponent<MenuGenerator>();
    }
}
