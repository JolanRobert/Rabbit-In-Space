using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class Food : MonoBehaviour {
    
    private Transform myPlantUI;
    public ItemType itemType;

    private Sprite seedSprite, foodSprite;
    private Sprite[] plantSprites;
    
    private int price;
    private int growthStage;
    private int decayTime;
    private Vector2 minMaxProduction;

    public int growthLevel;
    private int GrowthLevel {
        get => growthLevel;
        set {
            growthLevel = value;
            UIManager.Instance.GetGardenUI().SetPlantSprite(myPlantUI,plantSprites[value]);
        }
    }

    private int growingTime;
    private int GrowingTime {
        get => growingTime;
        set {
            growingTime = value;
            if (growingTime == growthStage) GrowthLevel = 1;
            UIManager.Instance.GetGardenUI().SetPlantGrowthTime(myPlantUI,value);
        }
    }

    private string foodName {
        set {
            UIManager.Instance.GetGardenUI().SetPlantName(myPlantUI,value);
        }
    }

    public void CreateFood(Transform plant) {
        myPlantUI = plant;
    }

    public void InitFood(FoodSO foodSo) {
        StopAllCoroutines();
        
        foodName = foodSo.name;
        itemType = foodSo.itemType;
        
        seedSprite = foodSo.seedSprite;
        plantSprites = foodSo.plantSprites;
        foodSprite = foodSo.foodSprite;
        
        price = foodSo.price;
        growthStage = foodSo.growingTime / 2;
        GrowingTime = foodSo.growingTime;
        decayTime = foodSo.decayTime;
        minMaxProduction = foodSo.minMaxProduction;

        GrowthLevel = 0;
        StartCoroutine(Growth());
    }
    
    private WaitForSeconds oneSec = new WaitForSeconds(1);
    private IEnumerator Growth() {
        while (GrowingTime > 0) {
            yield return oneSec;
            GrowingTime--;
        }

        GrowthLevel = 2;
        Harvest();
        StartCoroutine(Decay());
    }

    private IEnumerator Decay() {
        while (GrowingTime != -decayTime) {
            yield return oneSec;
            GrowingTime--;
        }
        
        GrowthLevel = 3;
    }

    public void Harvest() {
        int randomValue = Random.Range((int)minMaxProduction.x, (int)minMaxProduction.y+1);
        Debug.Log("Harvest "+itemType+" : "+randomValue);
    }
}
