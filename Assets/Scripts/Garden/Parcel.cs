using System;
using System.Collections.Generic;
using UnityEngine;

public class Parcel : InteractableElement {
    
    public Food[] foodList;

    public List<ParcelUpgradeEntry> upgradesUI;
    private int upgradeValue;
    
    public override void Interact() {
        GardenManager.Instance.SelectParcel(this);
        UIGarden.Instance.OpenParcelMenu();
    }

    public void InitUpgrades() {
        foreach (UpgradeType item in Enum.GetValues(typeof(UpgradeType))) {
            upgradesUI[(int)item].UnlockUpgrade(HasUpgrade(item));
        }
    }

    public void BuyUpgrade(UpgradeType upgradeType) {
        upgradeValue += (int)Mathf.Pow(2, (int)upgradeType);
        upgradesUI[(int)upgradeType].UnlockUpgrade(true);
    }

    public bool HasUpgrade(UpgradeType upgradeType) {
        int tmp_upgradeValue = upgradeValue;
        for (int i = Enum.GetValues(typeof(UpgradeType)).Length; i > (int)upgradeType; i--) {
            if (tmp_upgradeValue >= (int)Mathf.Pow(2, i)) tmp_upgradeValue -= (int)Mathf.Pow(2, i);
        }

        return tmp_upgradeValue >= (int) Mathf.Pow(2, (int) upgradeType);
    }
}
