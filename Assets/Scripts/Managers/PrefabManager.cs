using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public static PrefabManager Instance;
    [Header("FoodShop")]
    [SerializeField] private GameObject foodShopEntryPrefab;
    [SerializeField] private Transform foodShopEntryParent;

    [Header("ParcelUpgrade")]
    [SerializeField] private GameObject parcelUpgradeEntryPrefab;
    [SerializeField] private Transform parcelUpgradeEntryParent;
    
    [Header("Fridge/Workplan")]
    [SerializeField] private GameObject inventorySlotPrefab;
    [SerializeField] private Transform foodGroupParent;
    [SerializeField] private Transform recipeGroupParent;
    
    [Header("RecipeBook")]
    [SerializeField] private GameObject recipePanelPrefab;
    [SerializeField] private Transform recipePanelGroup;

    [Header("Directory")]
    [SerializeField] private GameObject customerEntryPrefab;
    [SerializeField] private Transform customerEntryGroup;

    void Awake()
    {
        Instance = this;
    }
    void Start() {
        foreach (FoodSO fSo in DataManager.Instance.foodList) {
            FoodShopEntry entry = Instantiate(foodShopEntryPrefab, foodShopEntryParent).GetComponent<FoodShopEntry>();
            entry.Init(fSo);
            
            InventorySlot slot = Instantiate(inventorySlotPrefab, foodGroupParent).GetComponent<InventorySlot>();
            slot.Init(fSo);
            UIKitchen.Instance.fridgeSlots.Add(slot);
        }
        
        foreach (ParcelUpgradeSO puSo in DataManager.Instance.parcelUpgradeList) {
            ParcelUpgradeEntry entry = Instantiate(parcelUpgradeEntryPrefab, parcelUpgradeEntryParent).GetComponent<ParcelUpgradeEntry>();
            entry.Init(puSo);
            UIGarden.Instance.upgrades.Add(entry);
        }

        foreach (RecipeSO rSo in KitchenManager.Instance.recipeList) {
            RecipePanel recipePanel = Instantiate(recipePanelPrefab, recipePanelGroup).GetComponent<RecipePanel>();
            recipePanel.SetupPanel(rSo);
            
            InventorySlot slot = Instantiate(inventorySlotPrefab, recipeGroupParent).GetComponent<InventorySlot>();
            slot.Init(rSo);
            UIKitchen.Instance.workplanSlots.Add(slot);
        }

        if (RecipeManager.Instance.currentRecipe != null)
        {
            recipePanelGroup.GetChild(RecipeManager.Instance.recipePanelIndex).GetComponent<RecipePanel>().SetAsRunning(RecipeManager.Instance.recipeAmount);
        }

        foreach (CustomerSO cSo in DataManager.Instance.customerList) {
            CustomerEntry customerEntry = Instantiate(customerEntryPrefab, customerEntryGroup).GetComponent<CustomerEntry>();
            customerEntry.Init(cSo);
        }
    }

    public void ResetRecipeBook()
    {
        recipePanelGroup.GetChild(0).GetComponent<RecipePanel>().SetAsInactive();
    }
}
