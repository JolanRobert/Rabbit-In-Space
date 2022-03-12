using System;
using System.Collections.Generic;
using System.Xml.Serialization;
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
    
    private void Awake()
    {
        switch (stockType)
        {
            case StockType.FRIDGE:
                if (fridgeInstance != null)
                {
                    return;
                }
                //DontDestroyOnLoad(gameObject);
                fridgeInstance = this;
                break;
            case StockType.WORKPLAN:
                if (workPlanInstance != null)
                {
                    return;
                }
                //DontDestroyOnLoad(gameObject);
                workPlanInstance = this;
                break;
        }
    }
    
    private void Start()
    {
        for (int i = 0; i < items.Count; i++)
        {
            slot = Instantiate(slotPrefab, Vector3.zero, Quaternion.identity, transform.GetChild(0).GetChild(0));
            LinkSlot(items[i].type, slot.GetComponent<InventorySlot>(), items[i].sprite);
        }
    }
    void LinkSlot(ItemType type, InventorySlot slot, Sprite sprite)
    {
        if (!slots.ContainsKey(type))
        {
            slots.Add(type, slot);
            slot.SetupSlot(type, sprite);
        }
    }
    public void OpenInventory()
    {
        inventory.SetActive(true);
    }
    [Serializable]
    public class FoodItem
    {
        public ItemType type;
        [XmlIgnore]
        public Sprite sprite;
        public int amount;
    }
}


