using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ParcelUpgradeEntry : MonoBehaviour {

    [Header("Actions")]
    [SerializeField] private Button touchableUpgrade;
    
    [Header("UI")]
    [SerializeField] private TMP_Text name;
    [SerializeField] private GameObject infos;
    [SerializeField] private GameObject banner;
    [SerializeField] private GameObject activeUpgrade;

    private UpgradeType upgradeType;
    private bool isActivable;

    public void Init(ParcelUpgradeSO puSo) {
        touchableUpgrade.onClick.AddListener(BuyUpgrade);
        
        name.text = puSo.name;
        infos.transform.GetChild(0).GetComponent<TMP_Text>().text = puSo.description;
        infos.transform.GetChild(1).GetComponent<TMP_Text>().text = puSo.unlockCost + "$";

        upgradeType = puSo.upgradeType;
        isActivable = puSo.isActivable;
    }

    private void BuyUpgrade() {
        GardenManager.Instance.myParcel.BuyUpgrade(upgradeType);
    }

    public void UnlockUpgrade(bool unlock) {
        if (unlock) {
            infos.SetActive(false);
            banner.SetActive(true);
            if (isActivable) activeUpgrade.SetActive(true);
        }
        else {
            infos.SetActive(true);
            banner.SetActive(false);
            if (isActivable) activeUpgrade.SetActive(false);
        }
    }

    public void SetActiveState(bool state) {
        
    }
}
