using UnityEngine;

[CreateAssetMenu(fileName = "New Food", menuName = "ScriptableObjects/Food")]
public class FoodSO : ScriptableObject
{
    public InventoryManager.ItemType type;
    public Sprite seedSprite;
    public Sprite[] plantSprites;
    public Sprite foodSprite;
    
    public int price;
    public int growthTime;
    public Vector2 minMaxProduction;
}
