using UnityEngine;

[CreateAssetMenu(fileName = "New Plant", menuName = "ScriptableObjects/Plant")]
public class PlantSO : ScriptableObject {

    public Sprite[] plantSprites;
    public int price;
    public int growthTime;
    public int waterUse;
}
