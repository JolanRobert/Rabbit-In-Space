using System.Collections.Generic;
using UnityEngine;

public class UIKitchen : MonoBehaviour {

    public static UIKitchen Instance;
    
    public List<InventorySlot> fridgeSlots = new List<InventorySlot>();
    public List<InventorySlot> workplanSlots = new List<InventorySlot>();

    void Awake() {
        Instance = this;
    }

    public void UpdateFridgeSlot(int slotIndex, int value) {
        fridgeSlots[slotIndex].UpdateAmount(value);
    }
    
    public void UpdateWorkplanSlot(int slotIndex, int value) {
        workplanSlots[slotIndex].UpdateAmount(value);
    }
}
