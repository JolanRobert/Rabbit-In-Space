using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ServiceManager : MonoBehaviour {

    public static ServiceManager Instance;
    
    [Header("Service Preparation")]
    [SerializeField] private List<ServiceEntry> serviceEntries;
    
    [SerializeField] private GameObject serviceValidPanel;
    [SerializeField] private GameObject serviceInvalidPanel;
    [SerializeField] private GameObject serviceWarningText;
    [SerializeField] private GameObject serviceConfirmEndPanel;

    [Header("Service Timer")]
    [SerializeField] [Range(1,500)] private int serviceTime = 360;
    [SerializeField] private ServiceTimer myTimer;

    [Header("Service Summary")]
    public ServiceSummary mySummary;

    [Header("Service Window")]
    [SerializeField] private MeshRenderer windowMR;

    void Awake() {
        if (Instance != null) Destroy(gameObject);
        else Instance = this;
    }

    void Start() {
        myTimer = ServiceTimer.Instance;
        mySummary = ServiceSummary.Instance;
    }

    public void LoadMenuFromButton() {
        if (KitchenManager.Instance.inService) {
            UIManager.Instance.OpenPanel(serviceConfirmEndPanel);
            return;
        }
        LoadMenu();
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

    public void StartService() {
        UIManager.Instance.ClosePanel(serviceValidPanel);
        
        KitchenManager.Instance.inService = true;
        KitchenManager.Instance.customerSpawner.StartService();

        UpdateWindow();

        myTimer.StartTimer(serviceTime);
        ServiceButton.Instance.UpdateMaterial();
    }

    public void EndService() {
        StartCoroutine(EndServiceCoroutine());
    }

    private IEnumerator EndServiceCoroutine() {
        KitchenManager.Instance.inService = false;
        KitchenManager.Instance.customerSpawner.EndService();
        ServiceButton.Instance.UpdateMaterial();
        
        UpdateWindow();
        yield return new WaitForSeconds(0.25f);
        
        CameraController.Instance.FocusElement(PlayerManager.Instance.transform);
        PlayerManager.Instance.GetAnimation().Haswon(true);
        yield return new WaitForSeconds(1);
        
        UIManager.Instance.CloseAllPanel();
        UIManager.Instance.OpenPanel(mySummary.summaryGO);
        mySummary.InitSummary();

        yield return new WaitForSeconds(1);
        StartCoroutine(mySummary.AnimSummary());
    }

    public void UpdateWindow() {
        windowMR.material.DOColor(KitchenManager.Instance.inService ? new Color(1, 1, 1, 0) : new Color(1, 1, 1, 1), 1);
    }
}
