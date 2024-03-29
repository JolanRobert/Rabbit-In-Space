using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour {

    public static DataManager Instance;
    
    public List<CustomerSO> customerList;
    public List<FoodSO> foodList;
    public List<ParcelUpgradeSO> parcelUpgradeList;
    public List<RecipeSO> recipeList;
    public List<StarSO> starList;

    void Awake() {
        if (Instance != null) Destroy(gameObject);
        else Instance = this;
    }
}
