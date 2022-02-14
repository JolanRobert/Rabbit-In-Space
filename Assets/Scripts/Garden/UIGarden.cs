using DG.Tweening;
using UnityEngine;

public class UIGarden : MonoBehaviour {

    public static UIGarden Instance;
    private Transform currentPanel;
    
    [SerializeField] private GameObject menuSeed;
    [SerializeField] private GameObject closeOverlay;

    void Awake() {
        Instance = this;
    }

    public void OpenParcel(Transform parcel) {
        currentPanel = parcel;
        currentPanel.gameObject.SetActive(true);
        currentPanel.DOScale(1, 0.325f);
    }

    public void CloseParcel() {
        currentPanel.DOScale(0, 0.325f).OnComplete(() => currentPanel.gameObject.SetActive(false));
        PlayerManager.Instance.GetInteract().isInteracting = false;
    }
    
    public void OpenMenuSeed(int foodSlot) {
        closeOverlay.SetActive(true);
        menuSeed.transform.DOMoveX(menuSeed.transform.position.x - 300, 0.325f);
        currentPanel.DOMoveX(currentPanel.transform.position.x - 150, 0.325f);
        GardenManager.Instance.SelectFood(foodSlot);
    }

    public void CloseMenuSeed() {
        closeOverlay.SetActive(false);
        menuSeed.transform.DOMoveX(menuSeed.transform.position.x + 300, 0.325f);
        currentPanel.DOMoveX(currentPanel.transform.position.x + 150, 0.325f);
    }
}
