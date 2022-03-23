using UnityEngine;

public class FoodDataManager : MonoBehaviour
{
    public static FoodDataManager Instance;
    [SerializeField] private DataSerializer dataSerializer;
    
    void Awake() {
        if (Instance != null) Destroy(gameObject);
        else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start() {
        foreach (FoodSO fSo in KitchenManager.Instance.foodList) {
            InventoryManager.FoodItem item = Load(fSo.foodType.ToString());
            item.amount = 50;
            Save(fSo.foodType.ToString(), item);
        }
    }
    
    public void AddItems(FoodType type, int amount) {
        InventoryManager.FoodItem item = Load(type.ToString());
        item.amount += amount;
        Save(type.ToString(), item);
    }

    public bool CheckItemQuantity(FoodType type, int amount) {
        if (type == FoodType.NONE) {
            Debug.LogWarning("Invalid type");
            return false;
        }
        
        InventoryManager.FoodItem item = Load(type.ToString());
        return amount <= item.amount;
    }

    public bool HasRecipeItem(RecipeType recipeType) {
        foreach (InventoryManager.RecipeItem item in InventoryManager.workplanInstance.serviceRecipes) {
            if (item.rSo.recipeType != recipeType) continue;
            return item.amount > 0;
        }

        return false;
    }
    
    private void Save(string fileName, InventoryManager.FoodItem foodItem) {
        dataSerializer.SaveData(fileName,foodItem); 
    }

    public InventoryManager.FoodItem Load(string fileName) {
        return dataSerializer.LoadData<InventoryManager.FoodItem>(fileName) ?? new InventoryManager.FoodItem();
    }
}
