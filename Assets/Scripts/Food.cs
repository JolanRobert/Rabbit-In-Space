using System;
using UnityEngine;

[Serializable]
public class Food {

    private GameObject myPlantUI;

    public ItemType itemType;

    private Sprite seedSprite, foodSprite;
    private Sprite[] plantSprites;
    
    private int price;
    private Vector2 minMaxProduction;

    private Sprite currentSprite {
        set {
            UIManager.Instance.GetGardenUI().SetPlantSprite(myPlantUI,value);
        }
    }

    private int growthTime {
        set {
            UIManager.Instance.GetGardenUI().SetPlantGrowthTime(myPlantUI,value);
        }
    }

    private string foodName {
        set {
            UIManager.Instance.GetGardenUI().SetPlantName(myPlantUI,value);
        }
    }

    private int growthLevel = 0;

    public Food(GameObject plant) {
        myPlantUI = plant;
    }

    public void InitFood(FoodSO foodSo) {
        foodName = foodSo.name;
        itemType = foodSo.itemType;
        
        seedSprite = foodSo.seedSprite;
        plantSprites = foodSo.plantSprites;
        foodSprite = foodSo.foodSprite;

        price = foodSo.price;
        growthTime = foodSo.growthTime;
        minMaxProduction = foodSo.minMaxProduction;

        currentSprite = plantSprites[growthLevel];
    }
}
