using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(InventoryManager))]
public class InventoryDebugger : Editor
{
    private ItemType itemType = ItemType.MOON_RICE;
    private int amount = 1;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        if (!Application.isPlaying)
        {
            return;
        }
        
        InventoryManager inventoryManager = (InventoryManager) target;
        
        itemType = (ItemType)EditorGUILayout.EnumPopup("Item :", itemType);
        amount = EditorGUILayout.IntField("Amount :", amount);
        if (GUILayout.Button("Add Item"))
        {
          inventoryManager.AddItems(itemType,amount);
        }
    }
}
