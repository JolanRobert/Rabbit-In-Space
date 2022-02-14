using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FoodUI {
    
    private Food myFood;

    private Image plantSprite;
    private TMP_Text plantName;
    private Image growthFill;
    private Image deadFill;
    private TMP_Text growthText;

    public FoodUI(Food food) {
        myFood = food;
    }

    public void InitFoodUI(Transform plant) {
        plantSprite = plant.GetChild(0).GetComponent<Image>();
        plantSprite.GetComponent<Button>().onClick.AddListener(myFood.Harvest);
        plantName = plant.GetChild(1).GetComponent<TMP_Text>();
        growthFill = plant.GetChild(2).GetComponent<Image>();
        deadFill = plant.GetChild(3).GetComponent<Image>();
        growthText = plant.GetChild(4).GetComponent<TMP_Text>();
    }

    public void Growth(int growingTime) {
        growthFill.DOFillAmount(1, growingTime).SetEase(Ease.Linear);
    }

    public void Decay(int decayTime) {
        deadFill.DOFillAmount(1, decayTime).SetEase(Ease.Linear);
    }

    public void UpdatePlantSprite(Sprite newSprite) {
        plantSprite.sprite = newSprite;
        plantSprite.SetNativeSize();
    }

    public void UpdateGrowthText(string newText) {
        growthText.text = newText;
    }

    public void ResetFoodUI() {
        plantSprite.sprite = null;
        plantSprite.rectTransform.sizeDelta = new Vector2(300, 250);
        plantName.text = "";
        growthFill.DOKill();
        growthFill.fillAmount = 0;
        deadFill.DOKill();
        deadFill.fillAmount = 0;
        growthText.text = "";
    }
}
