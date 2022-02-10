using UnityEngine;

public class Plant : MonoBehaviour {
    
    private Sprite[] plantSprites;
    private int price;
    private int growthTime;
    private int waterUse;
    
    private int growthLevel = 1;
    private int waterLevel;

    public void PlantSeed(PlantSO plantSO) {
        plantSprites = plantSO.plantSprites;
        price = plantSO.price;
        growthTime = plantSO.growthTime;
        waterUse = plantSO.waterUse;
    }
}
