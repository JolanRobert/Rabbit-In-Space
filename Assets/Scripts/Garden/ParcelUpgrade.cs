using System;
using System.Collections.Generic;
using UnityEngine;

public class ParcelUpgrade : MonoBehaviour {

    public List<UpgradeItem> upgradeList;

    public void InitUI(Transform upgrades) {
        for (int i = 0; i < upgrades.childCount; i++) {
            //upgradeList.Add();
        }
    }

    public void BuyUpgrade(UpgradeType upgradeType) {
        
    }

    public void IsActive(UpgradeType upgradeType) {
        
    }

    [Serializable]
    public class UpgradeItem {
        public Transform upgradeUI;
        public UpgradeType upgradeType;
        public bool isActive;
    }
}
