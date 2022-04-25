using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public static UIManager Instance;
    private Stack<GameObject> panels = new Stack<GameObject>();
    
    void Awake() {
        Instance = this;
    }

    public void OpenPanel(GameObject go) {
        if (panels.Contains(go)) return; //If panel already open return
        
        PlayerManager.Instance.GetInteract().isInteracting = true;
        panels.Push(go);
        panels.Peek().transform.localScale = Vector3.zero;
        panels.Peek().SetActive(true);
        panels.Peek().transform.DOScale(1, 0.325f);
    }

    public void ClosePanel() {
        ClosePanel(false);
    }

    public void CloseAllPanel() {
        ClosePanel(true);
    }

    private void ClosePanel(bool allPanel) {
        GameObject toClose = panels.Peek();
        toClose.transform.DOScale(0, 0.325f).OnComplete(() => {
            toClose.gameObject.SetActive(false);
        });
        
        panels.Pop();
        if (panels.Count == 0) PlayerManager.Instance.GetInteract().isInteracting = false;
        else if (allPanel) CloseAllPanel();
    }

    public void SetVisible(GameObject go, bool isVisible) {
        go.SetActive(isVisible);
    }
}