using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static UIManager Instance;
    private List<GameObject> panels = new List<GameObject>();
    
    void Awake() {
        Instance = this;
    }

    public void OpenPanel(GameObject go) {
        if (go.activeSelf) return; //If panel already open return

        PlayerManager.Instance.GetInteract().isInteracting = true;
        panels.Add(go);
        go.transform.localScale = Vector3.zero;
        go.SetActive(true);
        go.transform.DOScale(1, 0.325f);
    }

    public void OpenOptions(GameObject go)
    {
        GameManager.Instance.timeElapsing = false;
        OpenPanel(go);
    }

    public void ClosePanel(GameObject go) {
        panels.Remove(go);
        go.transform.DOScale(0, 0.325f).OnComplete(() => {
            go.SetActive(false);
        });
        
        if (panels.Count == 0) PlayerManager.Instance.GetInteract().isInteracting = false;
    }

    public void CloseAllPanel() {
        while (panels.Count > 0) ClosePanel(panels[panels.Count-1]);
    }
}
