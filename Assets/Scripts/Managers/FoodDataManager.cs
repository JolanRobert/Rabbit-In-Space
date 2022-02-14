using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodDataManager : MonoBehaviour
{
    public static FoodDataManager instance;
    [SerializeField] private DataSerializer dataSerializer;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void Save(string fileName, InventoryManager.FoodItem fooditem)
    {
        dataSerializer.SaveData(fileName,fooditem); 
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
