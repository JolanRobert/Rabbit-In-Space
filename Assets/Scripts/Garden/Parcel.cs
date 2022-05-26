using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Parcel : MonoBehaviour {

    public GardenEntry gardenEntry;
    
    [Header("Data")]
    public Food[] foodList;
    [SerializeField] private List<Upgrade> upgradeList;

    

    void Start() {
        //Init upgrade list
        foreach (ParcelUpgradeSO puSo in DataManager.Instance.parcelUpgradeList) {
            upgradeList.Add(new Upgrade(puSo.upgradeType));
        }
    }

    
    
    //
    // UPGRADES
    //

    public void BuyUpgrade(UpgradeType upgradeType) {
        foreach (Upgrade up in upgradeList) {
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
        foreach (Upgrade up in upgradeList) {
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
        foreach (Upgrade up in upgradeList) {
            if (up.upgradeType != upgradeType) continue;
            return up.isBought;
        }

        return false;
    }

    public bool IsUpgradeActive(UpgradeType upgradeType) {
        foreach (Upgrade up in upgradeList) {
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
