using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "ScriptableObjects/Recipe")]
public class RecipeSO : ScriptableObject
{
    [Header("Global Infos")] 
    public RecipeType recipeType;
    
    [Header("Sprites")]
    public Sprite recipeSprite;

    [Header("Attributes")] 
    public List<RecipeElement> recipeElements;
    public List<StationSO> stations;
    public int price;
    
}

[Serializable]
public class RecipeElement {
    public FoodSO food;
    public int amount;
}
