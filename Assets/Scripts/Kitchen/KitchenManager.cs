using System.Collections.Generic;
using UnityEngine;

public class KitchenManager : MonoBehaviour {

    public static KitchenManager Instance;
    private MenuGenerator menuGenerator;

    public List<RecipeSO> recipeList;

    void Awake() {
        Instance = this;
        menuGenerator = GetComponent<MenuGenerator>();
    }

    void Start() {
        menuGenerator.GenerateMenu();
    }
}
