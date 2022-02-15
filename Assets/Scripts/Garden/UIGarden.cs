using DG.Tweening;
using UnityEngine;

public class UIGarden : MonoBehaviour {

    public static UIGarden Instance;

    private Transform myParcelUI;
    private Transform menuParcel, menuUpgrade;
    
    [SerializeField] private GameObject menuSeed;
    [SerializeField] private GameObject closeOverlay;

    void Awake() {
        Instance = this;
    }

    public void OpenMenuParcel(Transform parcelUI) {
        myParcelUI = parcelUI;
        menuParcel = parcelUI.GetChild(0);
        menuUpgrade = parcelUI.GetChild(1);
        
        menuParcel.gameObject.SetActive(true);
        menuParcel.DOScale(1, 0.325f);
    }

    public void CloseMenuParcel() {
        menuParcel.DOScale(0, 0.325f).OnComplete(() => {
            menuParcel.gameObject.SetActive(false);
            menuParcel = null;
        });
        
        PlayerManager.Instance.GetInteract().isInteracting = false;

        menuUpgrade = null;
    }

    public void OpenMenuUpgrade() {
        menuParcel.gameObject.SetActive(false);
        menuUpgrade.gameObject.SetActive(true);
    }

    public void CloseMenuUpgrade() {
        menuUpgrade.gameObject.SetActive(false);
        menuParcel.gameObject.SetActive(true);
    }
    
    public void OpenMenuSeed(int foodSlot) {
        closeOverlay.SetActive(true);
        menuSeed.transform.DOMoveX(menuSeed.transform.position.x - 300, 0.325f);
        myParcelUI.DOMoveX(myParcelUI.transform.position.x - 150, 0.325f);
        GardenManager.Instance.SelectFood(foodSlot);
    }

    public void CloseMenuSeed() {
        closeOverlay.SetActive(false);
        menuSeed.transform.DOMoveX(menuSeed.transform.position.x + 300, 0.325f);
        myParcelUI.DOMoveX(myParcelUI.transform.position.x + 150, 0.325f);
    }
}
