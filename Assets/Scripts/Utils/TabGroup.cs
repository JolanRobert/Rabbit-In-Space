using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour {

    [SerializeField] private List<TabElement> tabElements = new List<TabElement>();
    [SerializeField] private List<GameObject> objectsToSwap = new List<GameObject>();
    [SerializeField] private TabElement selectedTab;

    [SerializeField] private Color tabIdle;
    [SerializeField] private Color tabHover;
    [SerializeField] private Color tabActive;

    void Start() {
        if (selectedTab != null) OnTabSelected(selectedTab);
    }

    public void Subscribe(TabElement element) {
        tabElements.Add(element);
    }

    public void OnTabEnter(TabElement element) {
        ResetTabs();
        if (element != selectedTab) element.background.color = tabHover;
    }

    public void OnTabExit(TabElement element) {
        ResetTabs();
    }
    
    public void OnTabSelected(TabElement element) {
        selectedTab = element;
        ResetTabs();
        element.background.color = tabActive;

        int index = element.transform.GetSiblingIndex();
        for (int i = 0; i < objectsToSwap.Count; i++) {
            if (i == index) objectsToSwap[i].SetActive(true);
            else objectsToSwap[i].SetActive(false);
        }
    }

    private void ResetTabs() {
        foreach (TabElement element in tabElements) {
            if (element == selectedTab) continue;
            element.background.color = tabIdle;
        }
    }
}
