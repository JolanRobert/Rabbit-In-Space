using System.Collections.Generic;
using UnityEngine;

public class ServiceManager : MonoBehaviour {

    public static ServiceManager Instance;

    [SerializeField] private List<ServiceEntry> serviceEntries;
    
    [SerializeField] private GameObject serviceValidPanel;
    [SerializeField] private GameObject serviceInvalidPanel;
    [SerializeField] private GameObject serviceWarningText;

    void Awake() {
        Instance = this;
    }

    public void LoadMenu() {
        if (KitchenManager.Instance.inService) {
            EndService();
            return;
        }
        
        KitchenManager.Instance.myMenu.GenerateMenu();
        List<RecipeSO> myMenu = KitchenManager.Instance.myMenu.todayMenu;
        
        foreach (ServiceEntry item in serviceEntries) item.gameObject.SetActive(false);

        for (int i = 0; i < myMenu.Count; i++) {
            serviceEntries[i].gameObject.SetActive(true);
            serviceEntries[i].Init(myMenu[i]);
        }

        if (myMenu.Count == 0) {
            UIManager.Instance.OpenPanel(serviceInvalidPanel);
            return;
        }
        
        UIManager.Instance.SetVisible(serviceWarningText,myMenu.Count < 3);
        UIManager.Instance.OpenPanel(serviceValidPanel);
    }

    private void StartService() {
        KitchenManager.Instance.inService = true;
        KitchenManager.Instance.customerSpawner.StartService();
    }

    private void EndService() {
        KitchenManager.Instance.inService = false;
        KitchenManager.Instance.customerSpawner.EndService();
    }
}
