using DG.Tweening;
using UnityEngine;

public class UIGarden : MonoBehaviour {

    public static UIGarden Instance;
    private GameObject currentPanel;
    
    [SerializeField] private GameObject menuSeed;
    [SerializeField] private GameObject closeOverlay;

    void Awake() {
        Instance = this;
    }

    public void OpenParcel(GameObject go) {
        currentPanel = go;
        currentPanel.SetActive(true);
    }

    public void CloseParcel() {
        currentPanel.SetActive(false);
        currentPanel = null;
        PlayerManager.Instance.GetInteract().isInteracting = false;
    }
    
    public void OpenMenuSeed(int foodSlot) {
        closeOverlay.SetActive(true);
        menuSeed.transform.DOMoveX(1330, 0.325f).SetEase(Ease.OutBack, 1.87f);
        GardenManager.Instance.SelectFood(foodSlot);
    }

    public void CloseMenuSeed() {
        closeOverlay.SetActive(false);
        menuSeed.transform.DOMoveX(1630, 0.325f).SetEase(Ease.InBack, 1.87f);
    }
}
