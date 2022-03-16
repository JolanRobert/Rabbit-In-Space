using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FoodShopEntry : MonoBehaviour {

    [SerializeField] private Image foodSprite;
    [SerializeField] private TMP_Text foodName;
    [SerializeField] private TMP_Text foodPrice;
    [SerializeField] private TMP_Text growingTime;
    [SerializeField] private TMP_Text decayTime;
    [SerializeField] private TMP_Text foodProduction;
    
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
        GetComponent<Button>().onClick.AddListener(() => {
            GardenManager.Instance.PlantSeed(GetComponent<GetEnum>().itemType);
        });
    }
}
