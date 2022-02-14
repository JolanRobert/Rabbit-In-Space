using UnityEngine;

[CreateAssetMenu(fileName = "New Food", menuName = "ScriptableObjects/Food")]
public class FoodSO : ScriptableObject {

    [Header("Global Infos")]
    public new string name;
    public EnumManager.ItemType itemType;
    
    [Header("Sprites")]
    public Sprite seedSprite;
    public Sprite[] plantSprites;
    public Sprite foodSprite;
    
    [Header("Attributes")]
    public int price;
    public int growingTime;
    public int decayTime;
    public Vector2 minMaxProduction;
}
