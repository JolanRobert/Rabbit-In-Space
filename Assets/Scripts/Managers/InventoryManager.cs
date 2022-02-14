using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager fridgeInstance;
    public static InventoryManager workPlanInstance;
    [SerializeField] private StockType stockType;
    [SerializeField] public GameObject inventory;
    [SerializeField] private GameObject slotPrefab;
    private GameObject slot;
    [SerializeField] public List<FoodItem> items;
    public Dictionary<ItemType, InventorySlot> slots = new Dictionary<ItemType, InventorySlot>();

    public enum StockType
    {
        FRIDGE, WORKPLAN
    }
    public enum ItemType
    {
        NONE, MOONRICE, STARBERRY, NEBULAZUKI, STARBERRY_DAIFUKU, HANAMI_DANGOS, MOCHI_WAFFLE, STARBERRY_BUBBLE_TEA
    }
    private void Awake()
    {
        switch (stockType)
        {
            case StockType.FRIDGE:
                if (fridgeInstance != null)
                {
                    return;
                }
                DontDestroyOnLoad(gameObject);
                fridgeInstance = this;
                break;
            case StockType.WORKPLAN:
                if (workPlanInstance != null)
                {
                    return;
                }
                DontDestroyOnLoad(gameObject);
                workPlanInstance = this;
                break;
        }
    }
    private void Start()
    {
        for (int i = 0; i < items.Count; i++)
        {
            slot = Instantiate(slotPrefab, Vector3.zero, quaternion.identity, transform.GetChild(0).GetChild(0));
            items[i].amount = 0; //Lire save
            LinkSlot(items[i].type, slot.GetComponent<InventorySlot>(), items[i].sprite, items[i].amount);
        }
    }
    void LinkSlot(ItemType type, InventorySlot slot, Sprite sprite, int amount)
    {
        if (!slots.ContainsKey(type))
        {
            slots.Add(type, slot);
            slot.SetupSlot(type, sprite, amount);
        }
    }

    public void OpenInventory()
    {
        inventory.SetActive(true);
    }

    public bool AddItems(ItemType type, int amount)
    {
        if (type == ItemType.NONE)
        {
            Debug.Log("Invalid type");
            return false;
        }
        foreach (FoodItem item in items)
        {
            if (item.type == type)
            {
                if (item.amount + amount < 0)
                {
                    Debug.Log("Too few items");
                    return false;
                }
                
                item.amount += amount;
                slots[type].UpdateAmount(item.amount);
                //MAJ save file
                break;
            }
        }
        return true;
    }
    [Serializable]
    public class FoodItem
    {
        public ItemType type;
        public Sprite sprite;
        public int amount;
    }
}


