using UnityEngine;

public class KitchenUI : MonoBehaviour {

    public static KitchenUI Instance;
    private GameObject currentPanel;

    void Awake() {
        Instance = this;
    }

    public void OpenBook(GameObject go) {
        currentPanel = go;
        currentPanel.SetActive(true);
    }

    public void OpenFridge()
    {
        currentPanel = InventoryManager.fridgeInstance.inventory;
        InventoryManager.fridgeInstance.OpenInventory();
    }
    
    public void OpenWorkplan()
    {
        currentPanel = InventoryManager.workplanInstance.inventory;
        InventoryManager.workplanInstance.OpenInventory();
    }
    
    public void ClosePanel() {
        currentPanel.SetActive(false);
        currentPanel = null;
        PlayerManager.Instance.GetInteract().isInteracting = false;
    }
}
