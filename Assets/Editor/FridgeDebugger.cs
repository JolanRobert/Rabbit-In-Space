using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FridgeInventoryManager))]
public class FridgeDebugger : Editor
{
    private FridgeInventoryManager.ItemTypes itemType = FridgeInventoryManager.ItemTypes.MOONRICE;
    private int amount = 1;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        FridgeInventoryManager fridgeInventoryManager = (FridgeInventoryManager) target;
        
        itemType = (FridgeInventoryManager.ItemTypes)EditorGUILayout.EnumPopup("Ingredient:", itemType);
        amount = EditorGUILayout.IntField("Amount :", amount);
        if (GUILayout.Button("Add Item"))
        {
          fridgeInventoryManager.AddItems(itemType,amount);
        }
    }
}
