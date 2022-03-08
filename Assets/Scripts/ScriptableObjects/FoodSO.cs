using UnityEngine;

[CreateAssetMenu(fileName = "New Food", menuName = "ScriptableObjects/Food")]
public class FoodSO : ScriptableObject {

    [Header("Global Infos")]
    public new string name;
    public ItemType itemType;
    
    [Header("Sprites")]
    public Sprite foodSprite;
    public Sprite[] plantSprites;

    [Header("Attributes")]
    public int price;
    public int growingTime;
    public int decayTime;
    public Vector2 minMaxProduction;
}
