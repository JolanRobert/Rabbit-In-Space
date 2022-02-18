using TMPro;
using UnityEngine;

public class ParcelUpgradeEntry : MonoBehaviour {

    public UpgradeType upgradeType;
    
    private TMP_Text name, description, unlockCost;
    private GameObject upgradeInfos, banner;

    public bool isUnlock;

    void Awake() {
        upgradeInfos = transform.GetChild(0).gameObject;
        banner = transform.GetChild(1).gameObject;
        
        name = upgradeInfos.transform.GetChild(0).GetComponent<TMP_Text>();
        description = upgradeInfos.transform.GetChild(1).GetComponent<TMP_Text>();
        unlockCost = upgradeInfos.transform.GetChild(2).GetComponent<TMP_Text>();
    }
    
    public void Init(ParcelUpgradeSO puSo) {
        upgradeType = puSo.upgradeType;
        
        name.text = puSo.name;
        description.text = puSo.description;
        unlockCost.text = puSo.unlockCost + "$";
    }

    public void Unlock() {
        isUnlock = true;
        upgradeInfos.SetActive(false);
        banner.SetActive(true);
    }
}
