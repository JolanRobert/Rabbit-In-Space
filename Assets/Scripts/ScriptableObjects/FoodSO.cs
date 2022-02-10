using UnityEngine;

[CreateAssetMenu(fileName = "New Food", menuName = "ScriptableObjects/Food")]
public class FoodSO : ScriptableObject {

    public string foodName;
    public ItemType itemType;
    
    [Header("Sprites")]
    public Sprite seedSprite;
    public Sprite[] plantSprites;
    public Sprite foodSprite;
    
    [Header("Attributes")]
    public int price;
    public int growthTime;
    public Vector2 minMaxProduction;
}

public enum ItemType {
    NONE, MOON_RICE, STARBERRY, NEBULAZUKI
}
