using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[Serializable]
public class Food : MonoBehaviour {
    
    //UI
    private Image ui_sprite, ui_growthFill, ui_deadFill;
    private TMP_Text ui_name;
    private Slider ui_growthSlider;
    private TMP_Text ui_growthText;
    
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
            UIManager.Instance.GetGardenUI().SetPlantSprite(ui_sprite,plantSprites[value]);
        }
    }

    private int growingTime;
    private int GrowingTime {
        get => growingTime;
        set {
            growingTime = value;
            if (growingTime == growthStage) GrowthLevel = 1;
            UIManager.Instance.GetGardenUI().SetPlantGrowthTime(ui_growthText,value);
        }
    }

    private string foodName {
        set {
            UIManager.Instance.GetGardenUI().SetPlantName(ui_name,value);
        }
    }

    public void CreateFood(Transform plant) {
        ui_sprite = plant.GetChild(0).GetComponent<Image>();
        ui_name = plant.GetChild(1).GetComponent<TMP_Text>();
        ui_growthFill = plant.GetChild(2).GetComponent<Image>();
        ui_deadFill = plant.GetChild(3).GetComponent<Image>();
        ui_growthText = plant.GetChild(4).GetComponent<TMP_Text>();

        plant.GetChild(0).GetComponent<Button>().onClick.AddListener(Harvest);
    }

    public void InitFood(FoodSO foodSo) {
        ResetFood();
        
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
        ui_growthFill.DOFillAmount(1, GrowingTime).SetEase(Ease.Linear);
        
        while (GrowingTime > 0) {
            yield return oneSec;
            GrowingTime--;
        }

        ui_growthText.text = "Done";
        GrowthLevel = 2;
        StartCoroutine(Decay());
    }

    private IEnumerator Decay() {
        ui_deadFill.DOFillAmount(1, decayTime).SetEase(Ease.Linear);
        
        while (GrowingTime != -decayTime) {
            yield return oneSec;
            GrowingTime--;
        }

        ui_growthText.text = "Dead";
        GrowthLevel = 3;
    }

    public void Harvest() {
        if (growthLevel < 2) return;
        if (growthLevel == 2) {
            int randomValue = Random.Range((int)minMaxProduction.x, (int)minMaxProduction.y+1);
            Debug.Log("Harvest "+itemType+" : "+randomValue);
        }
        ResetFood();
    }

    public void ResetFood() {
        StopAllCoroutines();
        ui_sprite.sprite = null;
        ui_sprite.rectTransform.sizeDelta = new Vector2(300, 250);
        ui_name.text = "Plant";
        ui_growthFill.DOKill();
        ui_deadFill.DOKill();
        ui_growthFill.fillAmount = 0;
        ui_deadFill.fillAmount = 0;
        ui_growthText.text = "";
    }
}
