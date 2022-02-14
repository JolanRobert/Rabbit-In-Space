using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class Food : MonoBehaviour {

    private FoodUI myFoodUI;
    
    private string foodName;
    public ItemType itemType;

    private Sprite seedSprite, foodSprite;
    private Sprite[] plantSprites;
    
    private int price;
    private int growthStageOne;

    private int growingTime;
    private int GrowingTime {
        get => growingTime;
        set {
            growingTime = value;
            if (growingTime == growthStageOne) GrowthLevel = 1;
            if (growingTime > 0) myFoodUI.UpdateGrowthText(growingTime.ToString());
        }
    }
    
    private int decayTime;
    private Vector2 minMaxProduction;

    private int growthLevel;
    private int GrowthLevel {
        get => growthLevel;
        set {
            growthLevel = value;
            myFoodUI.UpdatePlantSprite(plantSprites[growthLevel]);
        }
    }

    public void InitFoodUI(Transform myPlantUI) {
        myFoodUI = new FoodUI(this);
        myFoodUI.InitFoodUI(myPlantUI);
    }

    public void InitFood(FoodSO foodSo) {
        StopAllCoroutines();
        myFoodUI.ResetFoodUI();
        
        foodName = foodSo.name;
        itemType = foodSo.itemType;
        
        seedSprite = foodSo.seedSprite;
        plantSprites = foodSo.plantSprites;
        foodSprite = foodSo.foodSprite;
        
        price = foodSo.price;
        growthStageOne = foodSo.growingTime / 2;
        GrowingTime = foodSo.growingTime;
        decayTime = foodSo.decayTime;
        minMaxProduction = foodSo.minMaxProduction;
        
        GrowthLevel = 0;
        StartCoroutine(Growth());
    }
    
    private WaitForSeconds oneSec = new WaitForSeconds(1);
    private IEnumerator Growth() {
        myFoodUI.Growth(GrowingTime);
        
        while (GrowingTime > 0) {
            yield return oneSec;
            GrowingTime--;
        }

        myFoodUI.UpdateGrowthText("Done");
        GrowthLevel = 2;
        StartCoroutine(Decay());
    }

    private IEnumerator Decay() {
        myFoodUI.Decay(decayTime);
        
        while (GrowingTime != -decayTime) {
            yield return oneSec;
            GrowingTime--;
        }

        myFoodUI.UpdateGrowthText("Dead");
        GrowthLevel = 3;
    }

    public void Harvest() {
        if (growthLevel < 2) return;
        if (growthLevel == 2) {
            int randomValue = Random.Range((int)minMaxProduction.x, (int)minMaxProduction.y+1);
            Debug.Log("Harvest "+randomValue+" "+itemType);
            //InventoryManager.fridgeInstance.AddItems(itemType, randomValue);
        }
        
        StopAllCoroutines();
        myFoodUI.ResetFoodUI();
        itemType = ItemType.NONE;
    }
}
