using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Parcel : MonoBehaviour {

    [Header("Animation")]
    [SerializeField] private RectTransform topSprite;
    [SerializeField] private float openTime = 0.5f;
    
    [Header("Data")]
    [SerializeField] private int parcelID;
    public Food[] foodList;

    private bool isOpening;

    public void OpenParcel() {
        if (isOpening) return;
        StartCoroutine(OpenParcelCR());
    }

    private IEnumerator OpenParcelCR() {
        isOpening = true;
        if (GardenManager.Instance.myParcel != null) {
            foreach (Food food in GardenManager.Instance.myParcel.foodList) {
                food.foodUI = null;
            }
        }

        topSprite.DOMoveY(topSprite.position.y + 25, openTime);
        yield return new WaitForSeconds(openTime);
        
        GardenManager.Instance.myParcel = this;
        UIGarden.Instance.OpenParcelMenu();
        yield return new WaitForSeconds(0.2f);
        isOpening = false;
    }

    public void CloseParcel() {
        topSprite.DOMoveY(topSprite.position.y - 25, openTime);
    }
    
    //
    // UPGRADES
    //

    public void BuyUpgrade(UpgradeType upgradeType) {
        foreach (Upgrade up in GardenManager.Instance.upgrades[parcelID]) {
            if (up.upgradeType != upgradeType) continue;
            up.isBought = true;
            up.isActive = true;
            break;
        }
        
        UIGarden.Instance.upgrades[(int)upgradeType].SetupUpgrade(true,true);

        if (upgradeType == UpgradeType.GRAINATOR) {
            foreach (ParcelMenuEntry pme in UIGarden.Instance.plants) {
                pme.ShowGrainator(true);
            }
        }
    }

    public void ToggleUpgrade(UpgradeType upgradeType, bool state) {
        foreach (Upgrade up in GardenManager.Instance.upgrades[parcelID]) {
            if (up.upgradeType != upgradeType) continue;
            up.isActive = state;
            break;
        }

        if (upgradeType == UpgradeType.GRAINATOR) {
            for (int i = 0; i < foodList.Length; i++) {
                UIGarden.Instance.plants[i].ShowGrainator(state);
                foodList[i].SetGrainatorFood(!state ? FoodType.NONE : foodList[i].grainatorFood);
            }
        }
    }

    public bool IsUpgradeBought(UpgradeType upgradeType) {
        foreach (Upgrade up in GardenManager.Instance.upgrades[parcelID]) {
            if (up.upgradeType != upgradeType) continue;
            return up.isBought;
        }

        return false;
    }

    public bool IsUpgradeActive(UpgradeType upgradeType) {
        foreach (Upgrade up in GardenManager.Instance.upgrades[parcelID]) {
            if (up.upgradeType != upgradeType) continue;
            return up.isActive;
        }

        return false;
    }

    [Serializable]
    public class Upgrade {
        public UpgradeType upgradeType;
        public bool isBought, isActive;

        public Upgrade(UpgradeType upgradeType) {
            this.upgradeType = upgradeType;
            isBought = false;
            isActive = false;
        }
    }
}
