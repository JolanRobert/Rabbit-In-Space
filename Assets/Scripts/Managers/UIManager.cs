using UnityEngine;

public class UIManager : MonoBehaviour {

    public static UIManager Instance;
    private GameObject currentPanel;

    void Awake() {
        Instance = this;
    }

    public void OpenPanel(GameObject go) {
        currentPanel = go;
        currentPanel.SetActive(true);
    }

    public void ClosePanel() {
        currentPanel.SetActive(false);
        currentPanel = null;
        PlayerManager.Instance.GetInteract().isInteracting = false;
    }

    public void OpenMinigame(GameObject go) {
        CameraManager.Instance.EnableCamera(CameraType.MINIGAME);
        OpenPanel(go);
    }

    public void OpenFridge()
    {
        currentPanel = InventoryManager.fridgeInstance.inventory;
        InventoryManager.fridgeInstance.OpenInventory();
    }
    public void OpenWorkplan()
    {
        currentPanel = InventoryManager.workPlanInstance.inventory;
        InventoryManager.workPlanInstance.OpenInventory();
    }

    public void CloseMinigame() {
        CameraManager.Instance.DisableCamera(CameraType.MINIGAME);
        ClosePanel();
    }
}
