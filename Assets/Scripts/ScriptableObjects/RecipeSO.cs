using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "ScriptableObjects/Recipe")]
public class RecipeSO : ScriptableObject
{
    [Header("Global Infos")] 
    public new string name;
    public ItemType itemType;
    
    [Header("Sprites")]
    public Sprite foodSprite;

    [Header("Attributes")] 
    public List<RecipeElement> recipeElements;
    public List<StationSO> stations;
    public int price;
    
}

[Serializable]
public class RecipeElement
{
    public FoodSO food;
    public int amount;
}
