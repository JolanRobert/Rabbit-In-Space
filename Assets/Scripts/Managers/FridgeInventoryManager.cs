using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeInventoryManager : MonoBehaviour
{
    public static FridgeInventoryManager instance;
    [SerializeField] private List<InventorySlot> inventorySlots;
    public Dictionary<ItemTypes, InventorySlot> slots = new Dictionary<ItemTypes, InventorySlot>();
    public Dictionary<ItemTypes, int> amounts = new Dictionary<ItemTypes, int>();

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
        LinkSlot(ItemTypes.MOONRICE,inventorySlots[0], 0); //Remplacer 0 par la lecture de save file
        LinkSlot(ItemTypes.STARBERRY,inventorySlots[1], 0);
        LinkSlot(ItemTypes.NEBULAZUKI,inventorySlots[2], 0);
    }
    public enum ItemTypes
    {
        NONE, MOONRICE, STARBERRY, NEBULAZUKI
    }

    void LinkSlot(ItemTypes type, InventorySlot slot, int amount)
    {
        slots.Add(type, slot);
        amounts.Add(type, amount);
        slot.SetupSlot(type, amount);
    }

    public void AddItems(ItemTypes type, int amount)
    {
        slots[type].AddItems(amount);
        //MAJ save file
    }
}
