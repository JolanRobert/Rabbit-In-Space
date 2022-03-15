using System;
using System.Collections.Generic;
using UnityEngine;

public class Parcel : InteractableElement {
    
    public Food[] foodList;

    public List<ParcelUpgradeEntry> upgradesUI;

    private List<Upgrade> upgrades = new List<Upgrade>();

    void Start() {
        foreach (ParcelUpgradeSO puSo in GardenManager.Instance.upgradeList) {
            upgrades.Add(new Upgrade(puSo.upgradeType));
        }
    }
    
    public override void Interact() {
        if (PlayerManager.Instance.GetInteract().isInteracting) return;
        GardenManager.Instance.SelectParcel(this);
        UIGarden.Instance.OpenParcelMenu();
    }

    public void InitGrainator() {
        foreach (Food food in foodList) {
            food.foodUI.ActiveGrainator(IsUpgradeBought(UpgradeType.GRAINATOR) && IsUpgradeActive(UpgradeType.GRAINATOR));
        }
    }

    public void InitUpgrades() {
        foreach (UpgradeType item in Enum.GetValues(typeof(UpgradeType))) {
            upgradesUI[(int)item].UnlockUpgrade(IsUpgradeBought(item));
        }
    }

    public void BuyUpgrade(UpgradeType upgradeType) {
        foreach (Upgrade up in upgrades) {
            if (up.upgradeType != upgradeType) continue;
            up.isBought = true;
            up.isActive = true;
            break;
        }
        
        upgradesUI[(int)upgradeType].UnlockUpgrade(true);
        if (upgradeType == UpgradeType.GRAINATOR) InitGrainator();
    }

    public void ActiveUpgrade(UpgradeType upgradeType, bool state) {
        foreach (Upgrade up in upgrades) {
            if (up.upgradeType != upgradeType) continue;
            up.isActive = state;
            break;
        }
    }

    public bool IsUpgradeBought(UpgradeType upgradeType) {
        foreach (Upgrade up in upgrades) {
            if (up.upgradeType != upgradeType) continue;
            return up.isBought;
        }

        return false;
    }

    public bool IsUpgradeActive(UpgradeType upgradeType) {
        foreach (Upgrade up in upgrades) {
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
