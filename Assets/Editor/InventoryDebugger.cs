using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FoodDataManager))]
public class InventoryDebugger : Editor {
    
    private FoodType foodType = FoodType.MOON_RICE;
    private RecipeType recipeType = RecipeType.HANAMI_DANGOS;
    private int amount = 1;
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        
        if (!Application.isPlaying) return;
        
        FoodDataManager foodDataManager = (FoodDataManager) target;
        
        foodType = (FoodType)EditorGUILayout.EnumPopup("Item :", foodType);
        amount = EditorGUILayout.IntField("Amount :", amount);
        if (GUILayout.Button("Add Food")) foodDataManager.AddItem(foodType,amount);
        
        recipeType = (RecipeType)EditorGUILayout.EnumPopup("Item :", recipeType);
        amount = EditorGUILayout.IntField("Amount :", amount);
        if (GUILayout.Button("Add Recipe")) foodDataManager.AddRecipe(recipeType,amount);
    }
}
