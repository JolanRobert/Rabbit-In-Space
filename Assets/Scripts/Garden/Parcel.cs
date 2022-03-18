using System;
using System.Collections.Generic;

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
        GardenManager.Instance.myParcel = this;
        UIGarden.Instance.OpenParcelMenu();
    }
    
    //
    // UPGRADES
    //

    public void InitUpgrades() {
        foreach (UpgradeType item in Enum.GetValues(typeof(UpgradeType))) {
            bool isBought = IsUpgradeBought(item);
            bool isActive = IsUpgradeActive(item);
            
            upgradesUI[(int)item].SetupUpgrade(isBought,isActive);
            
            if (item == UpgradeType.GRAINATOR) {
                for (int i = 0; i < foodList.Length; i++) {
                    UIGarden.Instance.plants[i].ShowGrainator(isActive);
                    foodList[i].SetGrainatorFood(isActive ? foodList[i].grainatorFood : FoodType.NONE);
                }
            }
        }
    }

    public void BuyUpgrade(UpgradeType upgradeType) {
        foreach (Upgrade up in upgrades) {
            if (up.upgradeType != upgradeType) continue;
            up.isBought = true;
            up.isActive = true;
            break;
        }
        
        upgradesUI[(int)upgradeType].SetupUpgrade(true,true);

        if (upgradeType == UpgradeType.GRAINATOR) {
            foreach (ParcelMenuEntry pme in UIGarden.Instance.plants) {
                pme.ShowGrainator(true);
            }
        }
    }

    public void ToggleUpgrade(UpgradeType upgradeType, bool state) {
        foreach (Upgrade up in upgrades) {
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
