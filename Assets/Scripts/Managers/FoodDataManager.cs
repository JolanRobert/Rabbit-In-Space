using UnityEngine;

public class FoodDataManager : MonoBehaviour
{
    public static FoodDataManager Instance;
    [SerializeField] private DataSerializer dataSerializer;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void Save(string fileName, InventoryManager.FoodItem fooditem)
    {
        dataSerializer.SaveData(fileName,fooditem); 
    }

    public bool AddItems(ItemType type, int amount)
    {
        if (type == ItemType.NONE)
        {
            Debug.LogWarning("Invalid type");
            return false;
        }
        InventoryManager.FoodItem item = Load(type.ToString());
        if (item.amount + amount < 0)
        {
            Debug.Log("Too few items");
            return false;
        }
        item.amount += amount;
        Save(type.ToString(), item);
        return true;
    }

    public InventoryManager.FoodItem Load(string fileName)
    {
        if (dataSerializer.LoadData<InventoryManager.FoodItem>(fileName) == default)
        {
            return new InventoryManager.FoodItem();
        }
        return dataSerializer.LoadData<InventoryManager.FoodItem>(fileName);
    }
}
