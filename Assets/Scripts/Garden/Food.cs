using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class Food : MonoBehaviour {

    private string foodName;
    public ItemType itemType;

    private Sprite foodSprite;
    private Sprite[] plantSprites;
    
    private int price;
    private int tmp_growingTime;
    private int growingTime;

    private int GrowingTime {
        get => growingTime;
        set {
            growingTime = value;
            if (growingTime == tmp_growingTime / 2) GrowthLevel = 1;
            UI_UpdateGrowthText(growingTime.ToString());
        }
    }
    
    private int decayTime;
    private Vector2 minMaxProduction;

    private int growthLevel;
    private int GrowthLevel {
        get => growthLevel;
        set {
            growthLevel = value;
            UI_UpdatePlantSprite();
        }
    }

    public void StartNewFood(FoodSO foodSo) {
        StopAllCoroutines();
        foodUI.Reset();
        
        foodName = foodSo.name;
        itemType = foodSo.itemType;
        
        plantSprites = foodSo.plantSprites;
        foodSprite = foodSo.foodSprite;
        
        price = foodSo.price;
        tmp_growingTime = foodSo.growingTime;
        GrowingTime = foodSo.growingTime;
        decayTime = foodSo.decayTime;
        minMaxProduction = foodSo.minMaxProduction;

        GrowthLevel = 0;
        
        StartCoroutine(Growth());
    }
    
    private WaitForSeconds oneSec = new WaitForSeconds(1);
    private IEnumerator Growth() {
        //foodUI.growthFill.DOFillAmount(1, growingTime).SetEase(Ease.Linear);
        
        while (growingTime > 0) {
            yield return oneSec;
            GrowingTime--;
        }

        UI_UpdateGrowthText("Done");
        GrowthLevel = 2;
        StartCoroutine(Decay());
    }

    private IEnumerator Decay() {
        //foodUI.deadFill.DOFillAmount(1, decayTime).SetEase(Ease.Linear);
        
        while (growingTime != -decayTime) {
            yield return oneSec;
            growingTime--;
        }

        UI_UpdateGrowthText("Dead");
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
        if (foodUI != null) foodUI.Reset();
        itemType = ItemType.NONE;
    }
    
    //
    // UI
    //
    
    public ParcelMenuEntry foodUI;

    public void InitFoodUI() {
        if (itemType == ItemType.NONE) {
            foodUI.Reset();
            return;
        }
        
        foodUI.plantSprite.sprite = plantSprites[GrowthLevel];
        foodUI.plantName.text = foodName;

        foodUI.growthText.text = GrowthLevel switch {
            2 => "Done",
            3 => "Dead",
            _ => growingTime.ToString()
        };
    }

    public void UI_UpdatePlantSprite() {
        if (foodUI == null) return;
        foodUI.plantSprite.sprite = plantSprites[GrowthLevel];
        foodUI.plantSprite.SetNativeSize();
    }
    
    public void UI_UpdatePlantName() {
        if (foodUI == null) return;
        foodUI.plantName.text = foodName;
    }

    public void UI_UpdateGrowthText(string text) {
        if (foodUI == null) return;
        foodUI.growthText.text = text;
    }
}
