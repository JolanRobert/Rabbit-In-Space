using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ParcelUpgradeEntry : MonoBehaviour {

    [Header("Actions")]
    [SerializeField] private Button touchableUpgrade;
    
    [Header("UI")]
    [SerializeField] private new TMP_Text name;
    [SerializeField] private Image upgradeImage;
    [SerializeField] private TMP_Text description;
    [SerializeField] private TMP_Text unlockCost;
    [SerializeField] private GameObject soldOut;
    [SerializeField] private TMP_Text activeUpgrade;

    private UpgradeType upgradeType;
    private bool isActivable;

    public void Init(ParcelUpgradeSO puSo) {
        touchableUpgrade.onClick.AddListener(OnClickUpgrade);
        
        name.text = puSo.name;
        upgradeImage.sprite = puSo.upgradeSprite;
        description.text = puSo.description;
        unlockCost.text = $"{puSo.unlockCost}$";

        upgradeType = puSo.upgradeType;
        isActivable = puSo.isActivable;
    }

    private void OnClickUpgrade() {
        if (GardenManager.Instance.myParcel.IsUpgradeBought(upgradeType)) {
            if (!isActivable) return;
            if (GardenManager.Instance.myParcel.IsUpgradeActive(upgradeType)) DisableUpgrade();
            else EnableUpgrade();
        }
        else {
            GardenManager.Instance.myParcel.BuyUpgrade(upgradeType);
        }
    }

    public void SetupUpgrade(bool unlock, bool active) {
        if (unlock) {
            description.gameObject.SetActive(false);
            unlockCost.gameObject.SetActive(false);
            soldOut.SetActive(true);
            if (isActivable) {
                activeUpgrade.gameObject.SetActive(true);
                if (active) EnableUpgrade();
                else DisableUpgrade();
            }
        }
        else {
            description.gameObject.SetActive(true);
            unlockCost.gameObject.SetActive(true);
            soldOut.SetActive(false);
            if (isActivable) {
                activeUpgrade.gameObject.SetActive(false);
            }
        }
    }

    private void EnableUpgrade() {
        activeUpgrade.text = "Active";
        activeUpgrade.color = new Color(85 / 255f, 195 / 255f, 5 / 255f, 1);
        GardenManager.Instance.myParcel.ToggleUpgrade(upgradeType,true);
    }

    private void DisableUpgrade() {
        activeUpgrade.text = "Inactive";
        activeUpgrade.color = new Color(185 / 255f, 15 / 255f, 5 / 255f, 1);
        GardenManager.Instance.myParcel.ToggleUpgrade(upgradeType,false);
    }
}
