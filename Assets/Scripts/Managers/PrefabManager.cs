using UnityEngine;

public class PrefabManager : MonoBehaviour {
    
    [Header("FoodShop")]
    [SerializeField] private GameObject foodShopEntryPrefab;
    [SerializeField] private Transform foodShopEntryParent;

    [Header("ParcelUpgrade")]
    [SerializeField] private GameObject parcelUpgradeEntryPrefab;
    [SerializeField] private Transform parcelUpgradeEntryParent;
    
    void Start() {
        foreach (FoodSO foodSo in DataManager.Instance.foodList) {
            GameObject entry = Instantiate(foodShopEntryPrefab, foodShopEntryParent);
            entry.GetComponent<FoodShopEntry>().Init(foodSo);
        }
        
        foreach (ParcelUpgradeSO puSo in DataManager.Instance.parcelUpgradeList) {
            GameObject entry = Instantiate(parcelUpgradeEntryPrefab, parcelUpgradeEntryParent);
            entry.GetComponent<ParcelUpgradeEntry>().Init(puSo);
            UIGarden.Instance.upgrades.Add(entry.GetComponent<ParcelUpgradeEntry>());
        }
    }
}
