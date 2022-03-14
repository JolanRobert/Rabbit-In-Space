using System;
using System.Collections.Generic;
using UnityEngine;

public class Parcel : InteractableElement {
    
    public Food[] foodList;

    public List<ParcelUpgradeEntry> upgradesUI;
    private List<Upgrade> upgrades = new List<Upgrade>();

    void Start() {
        foreach (UpgradeType item in Enum.GetValues(typeof(UpgradeType))) {
            upgrades.Add(new Upgrade(item));
        }
    }
    
    public override void Interact() {
        GardenManager.Instance.SelectParcel(this);
        UIGarden.Instance.OpenParcelMenu();
    }

    public void InitUpgrades() {
        foreach (Upgrade item in upgrades) {
            if (item.isActive) upgradesUI[(int)item.upgradeType].UnlockUpgrade();
            else upgradesUI[(int)item.upgradeType].LockUpgrade();
        }
    }

    public void BuyUpgrade(UpgradeType upgradeType) {
        foreach (Upgrade item in upgrades) {
            if (item.upgradeType != upgradeType) continue;
            item.isActive = true;
            break;
        }
        
        upgradesUI[(int)upgradeType].UnlockUpgrade();
    }

    [Serializable]
    public class Upgrade {
        public UpgradeType upgradeType;
        public bool isActive;

        public Upgrade(UpgradeType upgradeType) {
            this.upgradeType = upgradeType;
            isActive = false;
        }
    }
}
