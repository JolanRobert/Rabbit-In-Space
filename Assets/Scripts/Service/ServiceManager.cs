using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceManager : MonoBehaviour {

    public static ServiceManager Instance;
    
    [Header("Service Preparation")]
    [SerializeField] private List<ServiceEntry> serviceEntries;
    
    [SerializeField] private GameObject serviceValidPanel;
    [SerializeField] private GameObject serviceInvalidPanel;
    [SerializeField] private GameObject serviceWarningText;

    [Header("Service Timer")]
    [SerializeField] [Range(1,500)] private int serviceTime = 360;
    [SerializeField] private ServiceTimer myTimer;

    [Header("Service Summary")]
    public ServiceSummary serviceSummary;

    void Awake() {
        if (Instance != null) Destroy(gameObject);
        else Instance = this;
    }

    void Start() {
        myTimer = ServiceTimer.Instance;
    }

    public void LoadMenu() {
        if (KitchenManager.Instance.inService) {
            myTimer.EndTimer();
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
        
        serviceWarningText.SetActive(myMenu.Count < 3);
        UIManager.Instance.OpenPanel(serviceValidPanel);
    }

    private void StartService() {
        UIManager.Instance.ClosePanel(serviceValidPanel);
        //serviceSummary.ResetSummary();
        
        KitchenManager.Instance.inService = true;
        KitchenManager.Instance.customerSpawner.StartService();
        
        myTimer.StartTimer(serviceTime);
    }

    public void EndService() {
        StartCoroutine(EndServiceCoroutine());
    }

    private IEnumerator EndServiceCoroutine() {
        KitchenManager.Instance.inService = false;
        KitchenManager.Instance.customerSpawner.EndService();
        
        UIManager.Instance.OpenPanel(serviceSummary.gameObject);
        serviceSummary.InitSummary();

        yield return new WaitForSeconds(1);
        StartCoroutine(serviceSummary.AnimSummary());
    }
}
