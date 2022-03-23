using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class Food : MonoBehaviour {

    private Parcel myParcel;

    void Awake() {
        myParcel = GetComponent<Parcel>();
    }
    
    private string foodName;
    public FoodType foodType;

    private Sprite foodSprite;
    private Sprite[] plantSprites;
    
    private int price;
    private int m_growingTime;
    private int growingTime;

    private int GrowingTime {
        get => growingTime;
        set {
            growingTime = value;
            if (growingTime == m_growingTime / 2) GrowthLevel = 1;

            if (foodUI == null) return;
            if (growingTime > 0) {
                foodUI.UpdateGrowthText(growingTime.ToString());
                foodUI.UpdateGrowthFill(m_growingTime-GrowingTime,m_growingTime);
            }
            else if (growingTime <= 0) {
                foodUI.UpdateGrowthFill(m_growingTime-GrowingTime,m_growingTime);
                foodUI.UpdateDeadFill(-growingTime,decayTime);
            }
        }
    }

    private int decayTime;
    private Vector2 minMaxProduction;

    private int growthLevel;
    private int GrowthLevel {
        get => growthLevel;
        set {
            growthLevel = value;

            if (foodUI == null) return;
            foodUI.UpdatePlantSprite(plantSprites[growthLevel]);
            if (growthLevel == 2) foodUI.UpdateGrowthText("Done");
            else if (growthLevel == 3) foodUI.UpdateGrowthText("Dead");
        }
    }
    
    public void StartNewFood(FoodSO foodSo) {
        StopAllCoroutines();
        if (foodUI != null) foodUI.Reset();
        
        foodName = foodSo.name;
        foodType = foodSo.foodType;
        
        plantSprites = foodSo.plantSprites;
        foodSprite = foodSo.foodSprite;
        
        price = foodSo.price;
        m_growingTime = myParcel.IsUpgradeActive(UpgradeType.NUTRIMENTS) ? foodSo.growingTime/2 : foodSo.growingTime;
        GrowingTime = m_growingTime;
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
        if (myParcel.IsUpgradeActive(UpgradeType.RECOLTOUT)) Harvest();
        else StartCoroutine(Decay());
    }

    public void ReduceTime(int sec) {
        GrowingTime -= sec;
        if (GrowingTime == 0) {
            StopAllCoroutines();
            GrowthLevel = 2;
            if (myParcel.IsUpgradeActive(UpgradeType.RECOLTOUT)) Harvest();
            else StartCoroutine(Decay());
        }
    }

    private IEnumerator Decay() {
        while (GrowingTime != -decayTime) {
            yield return oneSec;
            GrowingTime--;
        }

        GrowthLevel = 3;
    }

    public bool Harvest() {
        if (growthLevel < 2) return false;
        if (growthLevel == 2) {
            int prodValue = Random.Range((int)minMaxProduction.x, (int)minMaxProduction.y+1);
            if (myParcel.IsUpgradeActive(UpgradeType.ENGRAIS)) prodValue += 2;
            Debug.Log("Harvest "+prodValue+" "+foodType);
            //InventoryManager.fridgeInstance.AddItems(itemType, randomValue);
        }
        
        StopAllCoroutines();
        if (foodUI != null) foodUI.Reset();
        foodType = FoodType.NONE;
        
        if (myParcel.IsUpgradeActive(UpgradeType.GRAINATOR) && grainatorFood != FoodType.NONE) {
            foreach (FoodSO foodSo in GardenManager.Instance.foodList) {
                if (foodSo.foodType != grainatorFood) continue;
                StartNewFood(foodSo);
                break;
            }
        }
        
        return true;
    }
    
    public ParcelMenuEntry foodUI;

    public void InitFoodUI() {
        foodUI.SetTouchEvent(this);
        
        if (foodType == FoodType.NONE) {
            foodUI.Reset();
            return;
        }
        
        foodUI.UpdatePlantSprite(plantSprites[GrowthLevel]);
        foodUI.UpdatePlantName(foodName);
        
        foodUI.UpdateGrowthFill(m_growingTime-GrowingTime,m_growingTime);
        foodUI.UpdateDeadFill(-growingTime,decayTime);
        
        if (GrowthLevel == 2) foodUI.UpdateGrowthText("Done");
        else if (GrowthLevel == 3) foodUI.UpdateGrowthText("Dead");
        else foodUI.UpdateGrowthText(growingTime.ToString());
    }
    
    public FoodType grainatorFood;
    public void SetGrainatorFood(FoodType foodType) {
        grainatorFood = foodType;
        foodUI.UpdateGrainator(foodType);
        
        //Si rien n'est en train de pousser, plante automatiquement
        if (this.foodType == FoodType.NONE && grainatorFood != FoodType.NONE) {
            foreach (FoodSO foodSo in GardenManager.Instance.foodList) {
                if (foodSo.foodType != grainatorFood) continue;
                StartNewFood(foodSo);
                break;
            }
        }
    }
}
