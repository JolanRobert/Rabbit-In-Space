using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FoodDataManager))]
public class InventoryDebugger : Editor {
    
    private FoodType foodType = FoodType.MOON_RICE;
    private RecipeType recipeType = RecipeType.HANAMI_DANGOS;
    private int foodAmount = 1;
    private int recipeAmout = 1;
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        
        if (!Application.isPlaying) return;
        
        FoodDataManager foodDataManager = (FoodDataManager) target;
        
        foodType = (FoodType)EditorGUILayout.EnumPopup("Item :", foodType);
        foodAmount = EditorGUILayout.IntField("Amount :", foodAmount);
        if (GUILayout.Button("Add Food")) foodDataManager.AddItem(foodType,foodAmount);
        
        recipeType = (RecipeType)EditorGUILayout.EnumPopup("Item :", recipeType);
        recipeAmout = EditorGUILayout.IntField("Amount :", recipeAmout);
        if (GUILayout.Button("Add Recipe")) foodDataManager.AddRecipe(recipeType,recipeAmout);
    }
}
