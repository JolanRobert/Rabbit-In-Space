using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : MonoBehaviour
{
    public void MoreIngredients() {
        foreach (FoodSO ingredient in KitchenManager.Instance.foodList) {
            FoodDataManager.Instance.AddItem(ingredient.foodType, 50);
        }
    }

    public void NextReputationLevel() {
        GameManager.Instance.GainXP(GameManager.Instance.GetXPLeft());
    }

    public void MoreGold() {
        GameManager.Instance.GainGold(1000);
    }
}
