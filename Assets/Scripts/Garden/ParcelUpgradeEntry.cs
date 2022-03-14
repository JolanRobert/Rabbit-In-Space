using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ParcelUpgradeEntry : MonoBehaviour {

    [Header("Scriptable")]
    [SerializeField] private ParcelUpgradeSO puSo;
    
    [Header("Actions")]
    [SerializeField] private Button touchableUpgrade;
    
    [Header("UI")]
    [SerializeField] private TMP_Text name;
    [SerializeField] private GameObject infos;
    [SerializeField] private GameObject banner;

    void Start() {
        touchableUpgrade.onClick.AddListener(BuyUpgrade);
        
        name.text = puSo.name;
        infos.transform.GetChild(0).GetComponent<TMP_Text>().text = puSo.description;
        infos.transform.GetChild(1).GetComponent<TMP_Text>().text = puSo.unlockCost + "$";
    }

    private void BuyUpgrade() {
        GardenManager.Instance.myParcel.BuyUpgrade(puSo.upgradeType);
    }
    
    public void LockUpgrade() {
        infos.SetActive(true);
        banner.SetActive(false);
    }
    
    public void UnlockUpgrade() {
        infos.SetActive(false);
        banner.SetActive(true);
    }
}
