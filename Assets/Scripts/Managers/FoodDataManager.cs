using UnityEngine;

public class FoodDataManager : MonoBehaviour
{
    public static FoodDataManager Instance;
    [SerializeField] private DataSerializer dataSerializer;
    
    private void Awake() {
        if (Instance != null) Destroy(gameObject);
        else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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
    
    private void Save(string fileName, InventoryManager.FoodItem foodItem) {
        dataSerializer.SaveData(fileName,foodItem); 
    }

    public InventoryManager.FoodItem Load(string fileName) {
        return dataSerializer.LoadData<InventoryManager.FoodItem>(fileName) ?? new InventoryManager.FoodItem();
    }
}
