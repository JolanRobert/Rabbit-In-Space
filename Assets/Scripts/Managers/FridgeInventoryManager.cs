using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeInventoryManager : MonoBehaviour
{
    public static FridgeInventoryManager instance;
    [SerializeField] public GameObject inventory;
    [SerializeField] private List<InventorySlot> inventorySlots;
    public Dictionary<ItemTypes, InventorySlot> slots = new Dictionary<ItemTypes, InventorySlot>();
    public Dictionary<ItemTypes, int> amounts = new Dictionary<ItemTypes, int>();
    public enum ItemTypes
    {
        NONE, MOONRICE, STARBERRY, NEBULAZUKI
    }
    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        DontDestroyOnLoad(gameObject);
        instance = this;
    }
    private void Start()
    {
        amounts.Add(ItemTypes.MOONRICE, 0); //Remplacer 0 par la lecture de save
        amounts.Add(ItemTypes.STARBERRY, 0);
        amounts.Add(ItemTypes.NEBULAZUKI, 0);
    }
    void LinkSlot(ItemTypes type, InventorySlot slot, int amount)
    {
        if (!slots.ContainsKey(type))
        {
            slots.Add(type, slot);
            slot.SetupSlot(type, amount);
        }
    }

    public void OpenInventory()
    {
        inventory.SetActive(true);
        LinkSlot(ItemTypes.MOONRICE,inventorySlots[0], amounts[ItemTypes.MOONRICE]);
        LinkSlot(ItemTypes.STARBERRY,inventorySlots[1], amounts[ItemTypes.STARBERRY]);
        LinkSlot(ItemTypes.NEBULAZUKI,inventorySlots[2], amounts[ItemTypes.NEBULAZUKI]);
    }

    public bool AddItems(ItemTypes type, int amount)
    {
        if (type == ItemTypes.NONE)
        {
            Debug.Log("Invalid type");
            return false;
        }
        if (slots[type].itemNumber + amount < 0)
        {
            Debug.Log("Too few items");
            return false;
        }
        amounts[type] += amount;
        slots[type].UpdateAmount(amounts[type]);
        //MAJ save file
        return true;
    }
}
