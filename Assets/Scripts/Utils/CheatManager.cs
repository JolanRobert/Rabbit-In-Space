using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : MonoBehaviour {

    [SerializeField] private int moreIngredientsAmount;
    [SerializeField] private int moreGoldAmount;
    
    public void MoreIngredients() {
        foreach (FoodSO ingredient in KitchenManager.Instance.foodList) {
            FoodDataManager.Instance.AddItem(ingredient.foodType, moreIngredientsAmount);
        }
    }

    public void NextReputationLevel() {
        GameManager.Instance.GainXP(GameManager.Instance.GetXPLeft());
    }

    public void MoreGold() {
        GameManager.Instance.GainGold(moreGoldAmount);
    }
}
