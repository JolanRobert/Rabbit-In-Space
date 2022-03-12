using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FoodShopEntry : MonoBehaviour {

    private Image foodSprite;
    private TMP_Text foodName, foodPrice, growingTime, decayTime, foodProduction;

    void Awake() {
        foodSprite = transform.GetChild(0).GetComponent<Image>();
        foodName = transform.GetChild(1).GetComponent<TMP_Text>();
        foodPrice = transform.GetChild(2).GetComponent<TMP_Text>();
        growingTime = transform.GetChild(4).GetComponent<TMP_Text>();
        decayTime = transform.GetChild(6).GetComponent<TMP_Text>();
        foodProduction = transform.GetChild(7).GetComponent<TMP_Text>();
        
        GetComponent<Button>().onClick.AddListener(() => {
            GardenManager.Instance.PlantSeed(GetComponent<GetEnum>());
        });
    }
    
    public void Init(FoodSO foodSo) {
        foodSprite.sprite = foodSo.foodSprite;
        //To modify
        foodSprite.SetNativeSize();
        foodSprite.GetComponent<RectTransform>().sizeDelta = foodSprite.GetComponent<RectTransform>().sizeDelta / 5;
        
        foodName.text = foodSo.name;
        foodPrice.text = foodSo.price + "$";
        growingTime.text = foodSo.growingTime + "s";
        decayTime.text = foodSo.decayTime + "s";
        foodProduction.text = "+" + foodSo.minMaxProduction.x + "-" + foodSo.minMaxProduction.y;

        GetComponent<GetEnum>().itemType = foodSo.itemType;
    }
}
