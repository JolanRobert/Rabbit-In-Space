using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ParcelMenuEntry : MonoBehaviour {

    [Header("ID")]
    public int foodSlot;
    
    [Header("Actions")]
    [SerializeField] private Button touchablePlant;
    
    [Header("UI")]
    [SerializeField] private Image plantSprite;
    [SerializeField] private TMP_Text plantName;
    [SerializeField] private Image growthFill;
    [SerializeField] private Image deadFill;
    [SerializeField] private TMP_Text growthText;
    [SerializeField] private Button newSeed;
    
    [Header("Special")]
    [SerializeField] private GameObject grainatorSelect;

    void Start() {
        newSeed.onClick.AddListener(() => {
            UIGarden.Instance.OpenMenuSeed(foodSlot);
        });
    }

    public void SetTouchEvent(Food food) {
        touchablePlant.onClick.RemoveAllListeners();
        touchablePlant.onClick.AddListener(() => {
            if (!food.Harvest()) food.ReduceTime(1);
        });
    }
    
    public void UpdatePlantSprite(Sprite newSprite) {
        plantSprite.sprite = newSprite;
        plantSprite.SetNativeSize();
    }
    
    public void UpdatePlantName(string newName) {
        plantName.text = newName;
    }

    public void UpdateGrowthFill(float timeElapsed, float totalTime) {
        growthFill.fillAmount = timeElapsed / totalTime;
    }

    public void UpdateDeadFill(float timeElapsed, float totalTime) {
        deadFill.fillAmount = timeElapsed / totalTime;
    }

    public void UpdateGrowthText(string newGrowthText) {
        growthText.text = newGrowthText;
    }

    public void ShowGrainator(bool state) {
        grainatorSelect.SetActive(state);
    }

    public void UpdateGrainator(FoodType foodType) {
        grainatorSelect.GetComponent<GrainatorMenu>().SetFoodSprite(foodType);
    }
    
    public void Reset() {
        plantSprite.sprite = null;
        plantSprite.rectTransform.sizeDelta = new Vector2(250, 250);
        
        plantName.text = "";
        
        growthFill.DOKill();
        growthFill.fillAmount = 0;
        
        deadFill.DOKill();
        deadFill.fillAmount = 0;
        
        growthText.text = "";
    }
}
