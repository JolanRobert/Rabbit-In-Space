using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FoodDataManager))]
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
        
        FoodDataManager foodDataManager = (FoodDataManager) target;
        
        itemType = (ItemType)EditorGUILayout.EnumPopup("Item :", itemType);
        amount = EditorGUILayout.IntField("Amount :", amount);
        if (GUILayout.Button("Add Item"))
        {
          foodDataManager.AddItems(itemType,amount);
        }
    }
}
