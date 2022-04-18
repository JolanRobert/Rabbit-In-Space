using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour {

    public static DataManager Instance;
    
    public List<CustomerSO> customerList;
    public List<FoodSO> foodList;
    public List<ParcelUpgradeSO> parcelUpgradeList;
    public List<RecipeSO> recipeList;
    public List<StarRepartitionSO> starRepartitionList;
    public List<StationSO> stationList;

    void Awake() {
        Instance = this;
    }
}
