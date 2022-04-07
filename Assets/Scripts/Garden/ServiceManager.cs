using System.Collections.Generic;
using UnityEngine;

public class ServiceManager : MonoBehaviour {

    public static ServiceManager Instance;

    [SerializeField] private List<ServiceEntry> serviceEntries;
    
    [SerializeField] private GameObject serviceValidPanel;
    [SerializeField] private GameObject serviceInvalidPanel;
    [SerializeField] private GameObject serviceWarningText;

    public bool inService;

    void Awake() {
        Instance = this;
    }

    public void LoadMenu() {
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

    public void StartService() {
        inService = true;
    }

    public void EndService() {
        inService = false;
    }
}
