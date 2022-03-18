using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FoodDataManager))]
public class InventoryDebugger : Editor {
    
    private FoodType itemType = FoodType.MOON_RICE;
    private int amount = 1;
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        
        if (!Application.isPlaying) return;
        
        FoodDataManager foodDataManager = (FoodDataManager) target;
        
        itemType = (FoodType)EditorGUILayout.EnumPopup("Item :", itemType);
        amount = EditorGUILayout.IntField("Amount :", amount);
        if (GUILayout.Button("Add Item")) foodDataManager.AddItems(itemType,amount);
    }
}
