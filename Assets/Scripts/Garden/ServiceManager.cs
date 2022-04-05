using System.Collections.Generic;
using UnityEngine;

public class ServiceManager : MonoBehaviour {

    public static ServiceManager Instance;

    [SerializeField] private List<ServiceEntry> serviceEntries;

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
            UIGarden.Instance.OpenServiceInvalid();
            return;
        }
        
        UIGarden.Instance.ShowWarning(myMenu.Count < 3);
        UIGarden.Instance.OpenServiceValid();
    }

    public void StartService() {
        
    }

    public void EndService() {
        
    }
}
