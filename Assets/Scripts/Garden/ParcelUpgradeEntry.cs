using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ParcelUpgradeEntry : MonoBehaviour {

    [Header("Actions")]
    [SerializeField] private Button touchableUpgrade;
    
    [Header("UI")]
    [SerializeField] private new TMP_Text name;
    [SerializeField] private GameObject infos;
    [SerializeField] private GameObject banner;
    [SerializeField] private TMP_Text activeUpgrade;

    private UpgradeType upgradeType;
    private bool isActivable;

    public void Init(ParcelUpgradeSO puSo) {
        touchableUpgrade.onClick.AddListener(OnClickUpgrade);
        
        name.text = puSo.name;
        infos.transform.GetChild(0).GetComponent<TMP_Text>().text = puSo.description;
        infos.transform.GetChild(1).GetComponent<TMP_Text>().text = puSo.unlockCost + "$";

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
            infos.SetActive(false);
            banner.SetActive(true);
            if (isActivable) {
                activeUpgrade.gameObject.SetActive(true);
                if (active) EnableUpgrade();
                else DisableUpgrade();
            }
        }
        else {
            infos.SetActive(true);
            banner.SetActive(false);
            if (isActivable) {
                activeUpgrade.gameObject.SetActive(false);
            }
        }
    }

    private void EnableUpgrade() {
        activeUpgrade.text = "Activé";
        activeUpgrade.color = Color.green;
        GardenManager.Instance.myParcel.ToggleUpgrade(upgradeType,true);
    }

    private void DisableUpgrade() {
        activeUpgrade.text = "Désactivé";
        activeUpgrade.color = Color.red;
        GardenManager.Instance.myParcel.ToggleUpgrade(upgradeType,false);
    }
}
