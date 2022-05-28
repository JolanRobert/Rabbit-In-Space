using UnityEngine;

public class ParcelMenu : MonoBehaviour {
    
    [Header("Upgrades")]
    [SerializeField] private GameObject fertilizers;
    [SerializeField] private GameObject nutrients;
    [SerializeField] private GameObject pluckItAll;
    [SerializeField] private GameObject seedinator;

    public void SetupUpgrades(Parcel parcel) {
        SetupUpgrade(parcel, fertilizers, UpgradeType.ENGRAIS);
        SetupUpgrade(parcel, nutrients, UpgradeType.NUTRIMENTS);
        SetupUpgrade(parcel, pluckItAll, UpgradeType.RECOLTOUT);
        SetupUpgrade(parcel, seedinator, UpgradeType.GRAINATOR);
    }

    private void SetupUpgrade(Parcel parcel, GameObject go, UpgradeType upgradeType) {
        if (parcel.IsUpgradeBought(upgradeType) && parcel.IsUpgradeActive(upgradeType)) {
            go.SetActive(true);
        }
        else go.SetActive(false);
    }
}
