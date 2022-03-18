using System.Collections.Generic;
using UnityEngine;

public class KitchenManager : MonoBehaviour {

    public static KitchenManager Instance;
    public MenuGenerator menuGenerator;

    public List<FoodSO> foodList;
    public List<RecipeSO> recipeList;
    public List<CustomerSO> customerList;

    void Awake() {
        Instance = this;
        menuGenerator = GetComponent<MenuGenerator>();
    }

    void Start() {
        menuGenerator.GenerateMenu();
    }
}
